
app.controller('ContactCtrl', ['$scope', 'ContactService',
    function ($scope, CrudService) {

        // Base Url 
        var baseUrl = '/api/Contact/';
        $scope.btnText = "Save";
        $(".addtitle").html("Add Contact");
        $scope.Id = 0;
        $scope.SaveUpdate = function () {
            var contact = {
                FirstName: $scope.firstName,
                LastName: $scope.lasttName,
                Email: $scope.email,
                PhoneNumber: $scope.phonenumber,
                Id: $scope.Id,
                Status: $scope.status
            }
            if ($scope.btnText == "Save") {
                var apiRoute = baseUrl;//+ 'SaveContact/';
                var saveContact = CrudService.post(apiRoute, contact);
                saveContact.then(function (response) {                    
                    showAlert("Data save successfully", false);
                    $scope.GetContacts();
                    $scope.Clear();
                }, function (error) {
                    console.log("Error: " + error);
                    showAlert("Something went wrong.Please try again in a bit", true);
                });
            }
            else {
                var apiRoute = baseUrl + "/" + contact.Id;//+ 'UpdateContact/';
                var UpdateContact = CrudService.put(apiRoute, contact);
                UpdateContact.then(function (response) {                    
                    showAlert("Data update successfully", false);
                    $scope.GetContacts();
                    $scope.Clear();
                }, function (error) {
                    console.log("Error: " + error);
                    showAlert("Something went wrong.Please try again in a bit", true);
                });
            }
        }
        $scope.GetContacts = function () {
            AppendLoader("#tblcontact");
            var apiRoute = baseUrl;// + 'GetContacts/';
            var contact = CrudService.getAll(apiRoute);
            contact.then(function (response) {
                RemoveLoader("#tblcontact");
                $scope.contacts = response.data;

            },
                function (error) {
                    RemoveLoader("#tblcontact");
                    console.log("Error: " + error);
                    showAlert("Something went wrong.Please try again in a bit", true);
                });
        }
        $scope.GetContacts();

        $scope.GetContactByID = function (dataModel) {           
            var apiRoute = baseUrl; //+ 'GetContactByID';
            var contact = CrudService.getbyID(apiRoute, dataModel.Id);
            contact.then(function (response) {
                $scope.Id = response.data.Id;
                $scope.firstName = response.data.FirstName;
                $scope.lasttName = response.data.LastName;
                $scope.email = response.data.Email;
                $scope.phonenumber = response.data.PhoneNumber;
                $scope.status = response.data.Status;
                $scope.btnText = "Update";
                $(".addtitle").html("Update Contact");
            },
                function (error) {
                    console.log("Error: " + error);
                    showAlert("Something went wrong.Please try again in a bit", true);
                });
        }

        $scope.DeleteContact = function (dataModel) {           
            var apiRoute = baseUrl + "/" + dataModel.Id;// + 'DeleteContact/' + dataModel.ContactID;
            var deleteContact = CrudService.delete(apiRoute);
            deleteContact.then(function (response) {                
                showAlert("Data delete successfully.", false);
                $scope.GetContacts();
                $scope.Clear();
            }, function (error) {
                console.log("Error: " + error);
                showAlert("Something went wrong.Please try again in a bit", true);
            });
        }

        $scope.Clear = function () {
            $scope.Id = 0;
            $scope.firstName = "";
            $scope.lasttName = "";
            $scope.email = "";
            $scope.phonenumber = "";
            $scope.status = false;
            $scope.btnText = "Save";
            $(".addtitle").html("Add Contact");
        }

    }]);
function AppendLoader(selector) {
    if ($(selector).parent().find('button').length == 0) {
        var button = CreateLoader();        
        $(selector).parent().append(button);
    }
}
function RemoveLoader(selector) {    
    $(selector).parent().find('button').remove();
}
function CreateLoader() {
    var button = document.createElement("button");
    button.className = 'btn btn-sm btn-primary';
    var span = document.createElement("span");
    span.className = 'glyphicon glyphicon-refresh glyphicon-refresh-animate';
    button.appendChild(span);
    var t = document.createTextNode("Loading...");
    button.appendChild(t);
    return button;
}
function showAlert(message, error) {
    if (error) {
        $("#success-alert").removeClass("alert-success").addClass("alert-danger");
    }
    else {
        $("#success-alert").removeClass("alert-danger").addClass("alert-success");
    }
    $(".message").html(message);
    $("#success-alert").fadeTo(1000, 500).slideUp(500, function () {
        $("#success-alert").slideUp(500);
    });

}