function Saveposition1() {
    var objSync = new AuthenticateLogin();
    var contentName = document.getElementById("ContentName").value;
    var masterContentName = '';
    for (var i = 0; i < globalAllContentStore.length; i++) {
        if (selectedContedName == globalAllContentStore[i].ContentName) {
            var type = globalAllContentStore[i].ContentType;
            if (type == "Ma") {
                masterContentName = selectedContedName;
            }
        }
    }
    
    objSync.Saveposition(new Array(
        new Array("contentName", selectedContedName),
         new Array("x", pos.x),
          new Array("y", pos.y),
           new Array("width", pos.width),
            new Array("height", pos.height)
        ));
}


function Login() {
    var objSync = new AuthenticateLogin();
    //string name, string email, string phoneNo, string gender, string country)
    var username = document.getElementById("Username").value;
        var password = document.getElementById("Password").value;

//    var username = 'KARTHIK';
//    var password = 'KARTHIKP';
    objSync.SendDetailsToServer(new Array(
        new Array("username", username),
        new Array("password", password)
        ));
}


function Register() {
    var objSync = new AuthenticateLogin();
    //string name, string email, string phoneNo, string gender, string country)
    var username = document.getElementById("rUsername").value;
    var password = document.getElementById("rPassword").value;
    var email = document.getElementById("Email").value;
    
    //    var username = 'KARTHIK';
    //    var password = 'KARTHIKP';
    objSync.RegisterDetailsToServer(new Array(
        new Array("username", username),
        new Array("password", password),
        new Array("email", email)
        ));
}

function GetContentPosition() {
    var objSync = new AuthenticateLogin();

    objSync.GetContentPosition(new Array(
        new Array("contentName", selectedContedName)
        ));
}
function AddContent() {
    var objSync = new AuthenticateLogin();
    var contentName = document.getElementById("ContentName").value;
    var masterContentName = '';
    for (var i = 0; i < globalAllContentStore.length; i++) {
        if (selectedContedName == globalAllContentStore[i].ContentName)
        {
            var type = globalAllContentStore[i].ContentType;
            if (type == "Ma") {
                masterContentName = selectedContedName;
            }
        }
    }
    objSync.CreateNewContent(new Array(
        new Array("contentName", contentName),
        new Array("masterContentName", masterContentName)
        ));
}

function ShowOnlineUsers() {
    var objSync = new AuthenticateLogin();
    objSync.GetAllOnlineUsers(new Array());
}

function ShowAllContent() {
    var objSync = new AuthenticateLogin();
    objSync.GetAllContent(new Array());
}

function SendMessage(toUser, message, id_number) {
    var objSync = new AuthenticateLogin();
    var fromUser = loggedinUser;
    var toUser = toUser;
    var localid = id_number;
    objSync.SendMessage(new Array(


            new Array("fromUser", fromUser),
            new Array("toUser", toUser),
            new Array("message", message)

            ), localid);
}

function RecentMessages(id_number) {
    var singnedInUser = loggedinUser;
    var objSync = new AuthenticateLogin();
    objSync.GetAllMessages(new Array(
            new Array("username", singnedInUser), new Array("chatUser", chatUser)
            ), id_number);
}





//function uploadFile(file) {
//    var objSync = new AuthenticateLogin();
//    var bytes = [], fr;
//    var url = baseUrl + "UploadFile";
////    upload(file, url);
////    bytes = file.getAsBinary();
////    for (var i = 0; i < file.size; ++i) {
////        bytes.push(file.charCodeAt(i));
//    //    }

////    var r = new FileReader();
////    r.onload = function () {
////        bytes = r.result; 
////     };
//    //    r.readAsBinaryString(file);
//    
////    var url = "http://localhost:58437/" + "FileUpload.ashx";
////    var fr = new FileReader();
////    fr.file = file;
////    fr.readAsDataURL(file);
//// var xhr = new XMLHttpRequest();
////  xhr.open('POST', url, true);
////  xhr.onload = function (e) {
////      var i;
////      i = i + 1;
////  };
////  xhr.setRequestHeader("Cache-Control", "no-cache");
////  xhr.setRequestHeader("X-Requested-With", "XMLHttpRequest");
////  xhr.setRequestHeader("X-File-Name", file.name);
////  var reader = new FileReader();
////  reader.onload = function () { };
////  reader.readAsDataURL(file);
////  xhr.send(reader.result);
//  

////  var boundary = this.generateBoundary();
////  var xhr = new XMLHttpRequest;

////  xhr.open("POST", url, true);
////  xhr.onreadystatechange = function () {
////      if (xhr.readyState === 4) {
////          alert(xhr.responseText);
////      }
////  };
////  var contentType = "multipart/form-data;" ;
////  xhr.setRequestHeader("Content-Type", contentType);

////  for (var header in this.headers) {
////      xhr.setRequestHeader(header, headers[header]);
////  }

//////  // here's our data variable that we talked about earlier
//////  var data = this.buildMessage(this.elements, boundary);

////  // finally send the request as binary data
////  xhr.sendAsBinary(file);

////    create_blob(file, function (blob_string) { alert(blob_string) });

////    function create_blob(file, callback) {
////        var reader = new FileReader();
////        reader.onload = function () { callback(reader.result) };
////        reader.readAsDataURL(file);
////    }

////    objSync.UploadFile(new Array(new Array("bytes", bytes)

////            ), 1, file);
//}

function upload(file, posturl, name) {
    var boundary = "--------XX" + Math.random();

    var req = new XMLHttpRequest();
    req.open("POST", posturl);
    req.setRequestHeader("Content-type", "multipart/form-data; boundary=" + boundary);
    req.setRequestHeader("Content-length", file.fileSize);
    req.onload = function (event) { alert(event.target.responseText); }

    var prefix = "--" + boundary + "\n" +
               "Content-Disposition: form-data; name=\"" + name + "\"; filename=\"" +
               file.leafName + "\"\n" +
               "Content-type: text/plain\n\n";
    var stream = IO.newInputStream(prefix, "multi");
    stream.appendStream(IO.newInputStream(file, ""));
    stream.appendStream(IO.newInputStream("\n--" + boundary + "\n", ""));

    req.send(stream);
}
