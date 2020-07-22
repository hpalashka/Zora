const connection = new signalR.HubConnectionBuilder()
    .withUrl("/texthub")
    .build();
//connection.start().catch(err => console.error(err));
var ConversationId = document.getElementById("ConversationId").value;
connection.start().then(() => {
    connection.invoke('JoinGroup', ConversationId).catch(err => console.error(err.toString()));
});

connection.on("ReceiveText", function (output) {

    $('#messagesList li:last-child').append(output);
    var container = document.getElementById('messagesList');
    container.scrollTop = container.scrollHeight;
});


function join() {

    connection.invoke("JoinGroup", ConversationId).catch(err => console.error(err));

}



document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    connection.invoke("BroadcastText", message, ConversationId).catch(err => console.error(err));
    event.preventDefault();
});


