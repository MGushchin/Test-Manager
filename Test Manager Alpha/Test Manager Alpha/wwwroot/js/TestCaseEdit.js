const hubConnection = new signalR.HubConnectionBuilder().withUrl("/TestCaseEditHub").build();

document.getElementById("save").addEventListener("click", function () {
    Save();
});

hubConnection.start()
    .then(function () {

        hubConnection.invoke("GetTestCaseData", document.getElementById("TestCaseId").innerHTML)
            .catch(function (err) {
                document.getElementById("response").innerText = "Error";
                return console.error(err.toString());
            });
        document.getElementById("response").innerText = `OK2`;
    })
    .catch(function (err) {
        return console.error(err.toString());
    });

// получение сообщения от сервера
hubConnection.on("ReceivedSteps", function (steps) {

    document.getElementById("response").innerText = `Started with testCase.Steps.length ${steps.length}`;
    for (let i = 0; i < steps.length; i++) {
        document.getElementById("response").innerText += `TestCase Id: ${steps[i].id} Action: ${steps[i].action} Expected Result: ${steps[i].expectedResult} \n`;
    }
});

// получение сообщения от сервера
hubConnection.on("ReceivedTestCase", function (testCase) {
    var table = document.getElementById("tblSteps");
    table.innerHTML = '';

    var row = table.insertRow();

    var cell = row.insertCell();

    row.insertCell();
    row.insertCell();
    row.insertCell();

    for (let i = 0; i < testCase.steps.length; i++) {
        var newRow = table.insertRow();

        var newCell;

        newCell = newRow.insertCell();
        newCell.setAttribute('class', 'Id');
        newCell.innerHTML = `${i+1}`;

        newCell = newRow.insertCell();
        newCell.setAttribute('class', 'ActionCell');
        newCell.innerHTML = `<input type='text' class='tableInputAction' value = '${testCase.steps[i].action}'>`;

        newCell = newRow.insertCell();
        newCell.setAttribute('class', 'ResultCell');
        newCell.innerHTML = `<input type='text' class='tableInputResult' value = '${testCase.steps[i].expectedResult}'>`;

        newCell = newRow.insertCell();
        newCell.setAttribute('class', 'OptionsCell');
        newCell.innerHTML = `<button id='btnUp${i}'class='commonButton'>\u2191</button><button id='btnDown${i}'class='commonButton'>\u2193</button><button id='btnDelete${i}' class='commonButton'>Delete</button>`;

    }
});

function Save() {

    document.getElementById("response").innerText += `StartedSave`;
    var stepsTable = document.getElementById("tblSteps");

    var rowLength = stepsTable.rows.length;

    var actions = [];
    var results = [];

    for (i = 1; i < rowLength; i++)
    {
        actions[i-1] = stepsTable.rows[i].cells[1].children[0].value;
        results[i-1] = stepsTable.rows[i].cells[2].children[0].value;
    }

    hubConnection.invoke("SaveSteps", document.getElementById("TestCaseId").innerHTML, actions, results)
}

