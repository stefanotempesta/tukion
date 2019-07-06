# Tukion

## Architecture Design
The solution is structured as a multi-tier application implementing a clear separation of concerns between presentation, service and data access layers.\
![Architecture Diagram](https://github.com/stefanotempesta/tukion/blob/master/docs/ArchitectureDiagram.png)

This Visual Studio solution consists of the following projects:
- evenito.Tukion.Web: ASP.NET Core MVC frontend
- evenito.Tukion.API: ASP.NET Core API
- evenito.Tukion.Server: .NET Core Class Library backend

## evenito.Tukion.Web
The web frontend displays a list of videos retrieved from the API.

#### appsettings.json
The API base URI is configured as
`
"ApiBaseUri": "https://evenitotukionapi20190706050841.azurewebsites.net/"
`

#### Controllers > HomeController.cs
The Home controller displays the Index home page.\
Access to the appsettings.json file is by injecting the IConfiguration dependency in the constructor.
```csharp
public class HomeController : Controller
{
    private IConfiguration _configuration;

    public HomeController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
```

The Index action runs asynchronously to retrieve a list of available videos from the API.
Also the API is invoked asynchronously for not holding up the user interface.
The API service is disposable as it implements a stateless HTTP connection to the API, which is closed after each execution of the Index action.
```csharp
public async Task<IActionResult> Index()
{
    string apiBaseUrl = _configuration["ApiBaseUri"];
    using (var api = new ApiService(apiBaseUrl))
    {
        var videos = await api.GetVideos();

        return View(videos);
    }
}
```

#### Views > Home > Index.cshtml
The Index view enumerates a model that describes a video to display in the web page.
Data binding is performed server-side within the MCV controller.
Bootstrap is used for styling the web page.
```csharp
@model IEnumerable<evenito.Tukion.Server.Models.VideoModel>
```

#### Services > ApiService.cs
The API service client is a utility class for instantiating an HTTP client connection to the API service.
Currently the API does not require authentication. When available, an authentication token should be added to the API request's header.
```csharp
public class ApiService : IDisposable
{
    private HttpClient client;

    public ApiService(string baseUri)
    {
        client = new HttpClient();
        client.BaseAddress = new Uri(baseUri);
```

The GetVideos methods run asynchronously and returns a list of videos obtained from the API.
The API returns a JSON array of VideoModel objects, which is deserialized and converted to a collection of VideoModel.
```csharp
public async Task<IEnumerable<VideoModel>> GetVideos()
{
    var message = await client.GetAsync("api/videos");
    if (!message.IsSuccessStatusCode)
    {
        throw new InvalidOperationException(message.StatusCode.ToString());
    }

    string response = await message.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<IEnumerable<VideoModel>>(response);
}
```

The HTTP client connection is closed at the end of the execution of the API call.
This is done automatically in the Dispose method, which checks also that the client object is not null.
```csharp
public void Dispose()
{
    client?.Dispose();
}
```

## evenito.Tukion.API
The VideosController class implements a REST API for managing videos.
Currently, only the GET verb is implemented in two variants.\
\
Return all available videos.
```csharp
[HttpGet]
public ActionResult<IEnumerable<VideoModel>> Get()
{
    return _dataAdaptor.LoadAll().ToList();
}
```

Return a specific video by its ID.
```csharp
[HttpGet("{id}")]
public ActionResult<VideoModel> Get(Guid id)
{
    return _dataAdaptor.Load(id);
}
```

The VideosController is implemented as an API controller available at the route "api/videos".
The dependency to the data access layer is injected at runtime into the constructor (inversion of control pattern).
```csharp
[Route("api/[controller]")]
[ApiController]
public class VideosController : ControllerBase, IDisposable
{
    public VideosController(IDataAdaptor<VideoModel> dataAdaptor)
    {
        _dataAdaptor = dataAdaptor;
    }

    private IDataAdaptor<VideoModel> _dataAdaptor;
```

The implementation type for the IDataAdaptor interface is defined in the Startup object.
```csharp
services.AddTransient<IDataAdaptor<VideoModel>, MockVideoModelDataAdaptor>();
```

## evenito.Tukion.Server
The Server application implements business logic and data access layer,
and contains the definition of all the entities used for storage and a the video model used by the API and client application.

#### Entities
All main entities implement the IEntity interface, which defines a single Id field as primary key of type GUID.
```csharp
public interface IEntity
{
    Guid Id { get; set; }
}
```

The Server project defines the following entities:
- User
- Channel
- Video
- ChannelVideo: Many-to-many relationship between channels and videos
- VideoVisibility: Enumeration of visibility states for a video (Public, Private, Not Listed)
- Tag
- VideoTag: Many-to-many relationship between videos and tags
- Reaction
- ReactionType: Enumeration of different types of reactions (Like, Dislike, Love, Happy, Confused, Angry)
- Favourite
- Comment
- View

#### Entity Class Diagram
![Entity Class Diagram](https://github.com/stefanotempesta/tukion/blob/master/docs/EntityClassDiagram.png)

#### Data > IDataAdaptor.cs
The IDataAdaptor interface describes the CRUD methods for the data access layer.
The interface is generic for any entity type.
Currently, only query methods (Load and LoadAll) are defined.

```csharp
public interface IDataAdaptor<T> : IDisposable where T : new()
{
    IEnumerable<T> LoadAll();

    T Load(Guid id);
}
```

#### Data > Mocks > MockVideoModelDataAdaptor.cs
There is no persistent data storage in the current solution.
Mock data is exposed by the MockVideoModelDataAdaptor class, which implemented the IDataAdaptor interface for the specific VideoModel object.

```csharp
public class MockVideoModelDataAdaptor : IDataAdaptor<VideoModel>
```

#### Models > VideoModel.cs
The VideoModel object represents the combination of all relevant entities in a single object.
This object is used by the API for combining all the necessary information into a single payload
and avoid multiple round-trips between the web client and the API for obtaining all the necessary data to display on screen.

```csharp
public class VideoModel
{
    public Video Video { get; set; }

    public User Owner { get; set; }

    public IEnumerable<Tag> Tags { get; set; }

    public IEnumerable<View> Views { get; set; }

    public IEnumerable<Reaction> Reactions { get; set; }

    public IEnumerable<Favourite> Favourites { get; set; }

    public IEnumerable<Comment> Comments { get; set; }

    public IEnumerable<Channel> Channels { get; set; }

    // Format duration as HH:MM:SS
    public string DurationString => $"{(Video.Duration / 3600):00}:{(Video.Duration % 3600 / 60):00}:{(Video.Duration % 3600 % 60):00}";
}
```

## Deployment
The solution is deployed to the Azure cloud in two separate App Services for the Web client and the API.\
The physical separation allows for independent scalability of each component.\
Currently, aspects of high availability, disaster recovery, API management, caching, geographic redundancy and CDN are not implemented.


## Future Development
- Authenticate users.
- Secure the API with a token authentication.
- Separate entities and model in a common class library for web client and API (currently entities and models are defined in the Server project).
- Build a data access layer with persistent storage (currently data is mocked in memory).
- Implement commands for insert/update/delete of videos (currently the data model is read only).
- Segregate commands from queries (CQRS pattern).
- Switch to a GraphQL query language for the API.
- Move data binding to the client side for a SPA experience.
- Introduce exception handling, logging, event handling.
- Write unit, system, integrations, performance and security tests.
- Automate deployment (provisioning of infrastructure, build, testing and publishing of the solution).
