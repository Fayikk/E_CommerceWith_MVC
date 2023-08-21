var connectionChat = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chat").build();
document.getElementById("sendMessage").disabled = true;

var sender = document.getElementById("senderEmail").value;
var product = document.getElementById("productId").value;
console.log("deneme3");

document.getElementById("sendMessage").addEventListener("click", function (event) {
  
    var message = document.getElementById("chatMessage").value;
    var receiver = document.getElementById("receiverEmail").value;

    console.log(message);
    console.log(sender);
    console.log(receiver);
    console.log(product);
    if (message) {
        connectionChat.send("SendMessage", sender,message,receiver,product).then(function () {
            console.log("denem111e");
        }).catch(err => {
            console.error(err.toString())
        })

    }

    event.preventDefault();
})

connectionChat.on("ReceiverUser", (sender,message,product)=>{
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = `${sender} - ${message} - ${product}`;
});
connectionChat.start().then(function () {
    document.getElementById("sendMessage").disabled = false;
})