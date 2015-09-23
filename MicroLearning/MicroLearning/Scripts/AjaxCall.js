AuthenticateLogin.prototype.SendDetailsToServer = function (parameters, localid) {

    var url = baseUrl + "SignUpUser";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.SignUpUserResult;
        var values = eval("(" + result + ")");
        if (values.Status == "true") {

            loggedinUser = values.SignedInUserName;
            ShowElement('disp');
            HideElement('loginRow');
            GetOnlineUserTimerStart();
            HideElement('MessagesBoxTable');
            ShowAllContent();
            //            ShowOnlineUsers();
        }
        else {
            alert("Message sending Fail! Please try again");
        }
    }, parameterString);

    ajaxRequestObject.MakeRequest();
}

AuthenticateLogin.prototype.RegisterDetailsToServer = function (parameters, localid) {

    var url = baseUrl + "RegisterUser";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.RegisterUserResult;
        var values = eval("(" + result + ")");
        if (values.Status == "true") {
            var username = document.getElementById("rUsername").value;
            var password = document.getElementById("rPassword").value;
            var objSync = new AuthenticateLogin();
            objSync.SendDetailsToServer(new Array(
                new Array("username", username),
                new Array("password", password)
                ));
        }
        else {
            alert("Message sending Fail! Please try again");
        }
    }, parameterString);

    ajaxRequestObject.MakeRequest();
}



AuthenticateLogin.prototype.SendMessage = function (parameters, localId) {

    var url = baseUrl + "SendMessage";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.SendMessageResult;
        if (result == "true") {
            SentMessage(localId);
            RecentMessages(localId);
        }
        else {

        }

    }, parameterString);

    ajaxRequestObject.MakeRequest();

}

AuthenticateLogin.prototype.GetAllOnlineUsers = function (parameters, localId) {

    var url = baseUrl + "GetAllOnlineUsers";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.GetAllOnlineUsersResult;
//        if (result == "true") {
        BuildOnlineUsersBox(result);
//        }
//        else {

//        }
    }, parameterString);

    ajaxRequestObject.MakeRequest();
}


AuthenticateLogin.prototype.GetAllMessages = function (parameters, localId) {

    var url = baseUrl + "GetAllMessages";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.GetAllMessagesResult;
        //        if (result == "true") {
        BuildMessageBox(result);
        //        }
        //        else {

        //        }
    }, parameterString);

    ajaxRequestObject.MakeRequest();
}


AuthenticateLogin.prototype.GetAllContent = function (parameters, localId) {

    var url = baseUrl + "GetAllContent";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.GetAllContentResult;
        //        if (result == "true") {
        BuildAllContentBox(result);
        //        }
        //        else {

        //        }
    }, parameterString);

    ajaxRequestObject.MakeRequest();
}



AuthenticateLogin.prototype.CreateNewContent = function (parameters, localId) {

    var url = baseUrl + "CreateNewContent";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.CreateNewContentResult;
        if (result == "true") {
            CreatedContent(localId);
//            GetAllContent(localId);
        }
        else {

        }

    }, parameterString);

    ajaxRequestObject.MakeRequest();

}



AuthenticateLogin.prototype.UploadFile = function (parameters, localId, file) {

   var url = baseUrl + "UploadFile";
//        $.ajax({
//            url: url,

//            type: "POST",
//            data: file
//        });

    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.UploadFileResult;
        if (result == "true") {
            CreatedContent(localId);
            //            GetAllContent(localId);
        }
        else {

        }

    }, parameterString);

    ajaxRequestObject.MakeFileRequest(file);

}



AuthenticateLogin.prototype.Saveposition = function (parameters, localId) {

    var url = baseUrl + "Saveposition";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.SavepositionResult;
        if (result == "true") {
//            CreatedContent(localId);
            //            GetAllContent(localId);
        }
        else {

        }

    }, parameterString);

    ajaxRequestObject.MakeRequest();

}


AuthenticateLogin.prototype.GetContentPosition = function (parameters, localId) {

    var url = baseUrl + "GetContentPosition";
    var parameterString = "{";

    for (var i = 0; i < parameters.length; i++) {
        parameterString = parameterString + '"'
                  + parameters[i][0] + '":"'
                  + parameters[i][1] + '" ,';
    }

    parameterString = parameterString.slice(0, parameterString.length - 1);
    parameterString = parameterString + "}";

    var ajaxRequestObject = new AjaxRequest(url, "POST", function (responseText) {
        var jsonobj = eval('(' + responseText + ')');
        var result = jsonobj.GetContentPositionResult;
        var values = eval("(" + result + ")");
        if (values[0].Position != "") {
            var xxx = values[0].Position.split("_");
            pos.x = xxx[0];
            pos.y = xxx[1];
            pos.width = xxx[2];
            pos.height = xxx[3];
            sources.name = values[0].Image;
            loadImages1(sources, pos, initStage1);
           
            drawCanvas(selectedContedName);
            
        }
        else {
            pos.x = 0;
            pos.y = 0;
            pos.width = 200;
            pos.height = 160;
            loadImages1(sources, pos, initStage1);
            drawCanvas(selectedContedName);
        }

    }, parameterString);

    ajaxRequestObject.MakeRequest();

}

