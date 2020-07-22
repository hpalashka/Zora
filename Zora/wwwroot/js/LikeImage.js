var connection = new signalR.HubConnectionBuilder()
    .withUrl("/imagehub")
    .build();

connection.start();

connection.on("UpdateLikeCount", function (count, spanId, buttonId, liked) {
    //alert("update");
    document.getElementById(spanId).textContent = count;
    //change star
    if (liked === true) {
        document.getElementById(buttonId).innerHTML = "<i class=\"fas fa-star\"></i>";
    }
    else {
        document.getElementById(buttonId).innerHTML = "<i class=\"far fa-star\"></i>";
    }
});
$(".like-button").on("click", function () {
    var code = $(this).attr("data-id");
    //alert(code);
    connection.invoke("Like", parseInt(code)).catch(err => console.error(err));
});

 

