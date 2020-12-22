
let connection = new signalR.HubConnectionBuilder()
    .withUrl("/taskhub")
    .build();

connection.start()
    .catch(err => console.error(err.toString()));

connection.on("ReceiveTasksUpdate", function () {
    loadData();
});


function loadData() {
    var table = document.getElementById("tableBody");
    table.innerHTML = "";
    fetch("/Engineering/Engineering/AllTasksAsJson")
        .then(response => response.json())
        .then(data => data.forEach(task => {
            const tr = document.createElement("tr")
            tr.innerHTML = `<td><a href="/Engineering/Engineering/InstalationInfo?orderId=${task.orderId}">${task.id}</a></td>
                            <td>${task.instalationDateAsString}</td>
                            <td>${task.teamName}</td>`
            table.appendChild(tr);
        }))
};
