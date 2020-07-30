//var connection = new signalR.HubConnectionBuilder()
//    .withUrl("http://localhost:5017/notifications")
//    .build();

var connection = new signalR.HubConnectionBuilder()
    .configureLogging(signalR.LogLevel.Debug)
    .withUrl("https://localhost:5017/notifications", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
    })
    .build();


connection
    .start()
    .then(() => console.log('Connection started'))
    .catch((err) => console.log('Error while starting connection ' + err));

connection.on('ReceiveNotification', (data) => {
    console.log(data);
    alert("We have a new student in our school!");//todo add nice ui for this
 
  
});


