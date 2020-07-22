
const connection = new signalR.HubConnectionBuilder()
    .withUrl("/texthub")
    .build();
//connection.start().catch(err => console.error(err));
var ConversationId = document.getElementById("ConversationId").value;
connection.start().then(() => {
    connection.invoke('JoinGroup', ConversationId).catch(err => console.error(err.toString()));
});

connection.on("ReceiveText", function (text, user) {

    var msg = text.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    var container = document.getElementById('messagesList');
    container.insertBefore(li, container.firstChild);
});


function join() {
  
    connection.invoke("JoinGroup", ConversationId).catch(err => console.error(err));
  
}



document.getElementById("sendButton").addEventListener("click", function (event) {

    var message = document.getElementById("messageInput").value;
    document.getElementById("messageInput").value = "";
    connection.invoke("BroadcastTextGroup", message, ConversationId).catch(err => console.error(err));
    event.preventDefault();
});


