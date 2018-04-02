
var table = [];

function AddUser() {
    var user = GetNewUser();
    if (ValidUser(user)) {
        AddTableRow(user);
        HideErrorBox();
        ProcessTable();
        CleanForm();
        $("#Name").focus();
    } else {
        ShowErrorBox();
    }
}

function AutoRegister(event) {
    if (event.keyCode == 13) {
        AddUser();
    }
}

function AddTableRow(user) {
    table.push(user);
}

function ShowErrorBox() {
    $("#errorBox").show('slow');
}

function HideErrorBox() {
    $("#errorBox").hide('slow');
}

function WriteErrorMessage(msg) {
    $("#errorMessage").text(msg);
}

function ValidUser(user) {
    if (user.Name.length < 3) {
        WriteErrorMessage("Name is too short.");
        return false;
    }
    else if (!DateIsValid(user.Date)) {
        WriteErrorMessage("Date is invalid.");
        return false;
    } else if (user.Points.length == 0 || isNaN(user.Points.trim())) {
        WriteErrorMessage("Number of points is invalid.");
        return false;
    } else if (Number(user.Points) > 999999999) {
        WriteErrorMessage("Number of points can't be greater than '999999999'.");
        return false;
    }

    else {
        return true;
    }
}

function DateIsValid(dateStr) {
    try {

        if (dateStr == "" || dateStr.length != 10) return false;

        var date = new Date(dateStr);

        if (date.getFullYear() < 1900 || date.getFullYear() > 3000)
            return false;
        else return true;
    } catch (e) {
        return false;
    }
}

function ProcessTable() {
    $.ajax({
        url: '/Home/UpdateGrid',
        data: { aTable: JSON.stringify(table) },
        type: 'Post',
        success: function (response) {
            table = JSON.parse(response);
            BuildTable();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            WriteErrorMessage("Something went wrong, try again later.");
            ShowErrorBox();
        }
    });
}

function UpdatePoint(aValue, aIndexVal) {
    $.ajax({
        url: '/Home/UpdatePoint',
        data: { aTable: JSON.stringify(table), aQntPoint: aValue, aIndex: aIndexVal },
        type: 'Post',
        success: function (response) {
            table = JSON.parse(response);
            BuildTable();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            WriteErrorMessage("Something went wrong, try again later.");
            ShowErrorBox();
        }
    });
}

function BuildTable() {

    //Clean old rows before insert the new ones
    $("#tableClients tbody").remove();

    //Insert new rows based on the server return
    for (var i = 0; i < table.length; i++) {
        $("#tableClients").append(`<tr>
             <td> ${table[i].Position} </td>
             <td> ${table[i].Date.trim().substring(0, 10)} </td>
             <td> ${table[i].Name} </td>
             <td>
                <button type="button" class="btn btn-default btn-xs" onclick="MinusPoint(this);">
                    <span class="glyphicon glyphicon-minus" aria-hidden="true"></span>
                </button>
                ${table[i].Points} 
                <button type="button" class="btn btn-default btn-xs" onclick="PlusPoint(this);">
                    <span class="glyphicon glyphicon-plus" aria-hidden="true"></span>
                </button>
             </td>
             <td> <span class="glyphicon glyphicon-pencil" onclick='EditRow(this)'
                    aria-hidden="true" style='cursor:pointer'></span>
                  <span class="glyphicon glyphicon-trash" onclick='RemoveRow(this)'
                    aria-hidden="true" style='cursor:pointer'></span>
             </td>
             </tr>`);
    }
}

function GetRowIndex(domTableElement) {
    return Number($(domTableElement).parent().parent().find("td").eq(0).text()) - 1;
}

function PlusPoint(bntPlus) {
    var index = GetRowIndex(bntPlus);
    UpdatePoint(1, index);
}

function MinusPoint(bntMinus) {
    var index = GetRowIndex(bntMinus);
    UpdatePoint(-1, index);
}

function EditRow(bntEdit) {

    //Retrieving position of the removed row
    var index = GetRowIndex(bntEdit);

    $("#Name").val(table[index].Name);
    $("#Date").val(table[index].Date.trim().substring(0, 10));
    $("#Points").val(table[index].Points);

    //Removing row from the grid array
    table.splice(index, 1);

    ProcessTable();
}

function RemoveRow(bntRemove) {


    var name = $(bntRemove).parent().parent().find("td").eq(2).text().trim();

    if (confirm(`Delete ${name} ?`)) {
        //Retrieving position of the removed row
        var index = GetRowIndex(bntRemove)

        //Removing row from the grid array
        table.splice(index, 1);

        ProcessTable();
    }
}

function GetNewUser() {
    return {
        Name: $("#Name").val(),
        Date: $("#Date").val(),
        Points: $("#Points").val()
    };
}

function CleanForm() {
    $("#Name").val(""),
        $("#Date").val(""),
        $("#Points").val("")
}

function DelRow(bntDel) {

    var name = $(bntDel).parent().parent().find("td").eq(2).text().trim();

    if (confirm(`Delete ${name} ?`)) {
        $(bntDel).parent().parent().remove();
    }

}

function DonwloadFile(fileTypeArg) {
    if (table.length > 0) {

        $.ajax({
            url: '/Home/PrepareFile',
            data: { aTable: JSON.stringify(table), fileType: fileTypeArg },
            type: 'Post',
            success: function (response) {
                window.location = '/Home/DownloadFile'
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                WriteErrorMessage("Something went wrong, try again later");
                ShowErrorBox();
            }
        });
    } else {
        alert("Add rows on the grid before export your file.")
    }
}

function LoadTable(tblUploaded) {
    table = JSON.parse(tblUploaded)
    BuildTable(); 
}

function ClearTable()
{
    if (confirm("Clear table lines?")) {
        $("#tableClients tbody").remove();
    }
}

function ValidateUploadedFile(inputFile) {
    if ($(inputFile).val().split('.').pop().toUpperCase() != "JSON") {
        alert("Extension invalid, please insert a valid JSON file.")
        return false;
    }
}