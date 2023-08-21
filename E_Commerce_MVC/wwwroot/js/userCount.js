//Create Connection
var connectionUserCount = new signalR.HubConnectionBuilder().configureLogging(signalR.LogLevel.Information).withUrl("/hubs/userCount", signalR.HttpTransportType.WebSockets).build();
//connect to methods that hub inokes aka receive notifications from hub
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
});

connectionUserCount.on("updateTotalUsers", (value) => {
    var newCountSpan = document.getElementById("totalUsersCounter");
    newCountSpan.innerText = value.toString();
    toastr.success(`You have subscribed successfully. ${newCountSpan.innerText}`);
});
//invoke hub methods aka send notification to hub 
function newWindowLoadedOnClient() {
    connectionUserCount.invoke("NewWindowLoaded", "fayik").then((value) => console.log(value));
}


//start connection
function fulfilled() {
    console.log("Connection to User Hub Successfull")
    newWindowLoadedOnClient();
}

function rejected() {

}

connectionUserCount.start().then(fulfilled, rejected);