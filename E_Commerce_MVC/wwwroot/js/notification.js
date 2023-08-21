var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/notification").build();



const button = document.getElementById("notification");

button.addEventListener("click", function (event) {
    var text = document.getElementById("deneme").value;
    connectionChat.send("SendNotification", text).then(function () {
        console.log("denem111e");
    })
    event.preventDefault();
})


connectionChat.on("Notification", text => {
    toastr.success(text);
});


connectionChat.start();