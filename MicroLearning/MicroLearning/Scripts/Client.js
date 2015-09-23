function HideElement(x) {
    if (document.all) {
        document.getElementById(x).style.visibility = "hidden";
    } else {
        document.getElementById(x).style.display = "none";
    }
}

function drawCanvas(msg) {
    canvasLayer('container', 'textelement');
    var canvas = document.getElementById("textelement");
    var context = canvas.getContext('2d');
//     Gradient Text Fill and Stroke
//    var gradient = context.createLinearGradient(5, 50, 500, 20);
//    var gradient = context.createLinearGradient(0, 0, 0, 0);
//    gradient.addColorStop(0.1, '#F82E23');
//    gradient.addColorStop(0.25, '#E2E651');
//    gradient.addColorStop(0.5, '#55D268');
//    gradient.addColorStop(0.75, '#5A77DA');
//    gradient.addColorStop(1, '#C84489');
//    context.fillStyle = gradient;
//    context.font = "bold 36pt sans-serif";
//    context.shadowOffsetX = 5;
//    context.shadowOffsetY = 5;
//    context.shadowBlur = 10;
//    context.fillText("Gradient Fill Text", 20, 280);
//    context.fillStyle = '#000000';
//    context.strokeStyle = gradient;
//    context.lineWidth = 3;
//    context.strokeText("Gradient Stroke Text", 20, 350);

    context.font = "italic 16px Times";
    context.strokeText(msg, 15, 60);
}

function canvasLayer(location, id) {

//    if (layer.name) {
//        this.childrenNames[layer.name] = layer;
//    }
//    layer.canvas.width = this.width;
//    layer.canvas.height = this.height;
//    this._add(layer);

//    // draw layer and append canvas to container
//    layer.draw();
//    this.content.appendChild(layer.canvas);

    var cont = document.getElementById("container");

    var newCanvas = document.createElement('canvas');
//    newCanvas.height = "30";
//    newCanvas.width = "113";
    newCanvas.setAttribute('id', 'textelement');
    cont.appendChild(newCanvas);

    var context = newCanvas.getContext('2d');

//    this.width = $(window).width();
//    this.height = $(window).height();
//    this.element = document.createElement('canvas');

//    $(this.element)
//       .attr('id', id)
//       .text('unsupported browser')
//       .width(this.width)
//       .height(this.height)
//       .appendTo(location);

//    this.context = this.element.getContext("2d");
}

function ShowElement(x) {

//   var pos = {
//     x: 0,
//        y: 0,
//        width: 400,
//        height: 160
//        };
//    var sources = {
//        name: "/Images/darth-vader.jpg"
//        
//    };
//    loadImages1(sources, pos, initStage1);





    if (document.all) {
        document.getElementById(x).style.visibility = "visible";
    } else {
        document.getElementById(x).style.display = "table-row";
    }
}
function Log(msg) {
    document.getElementById('console').innerHTML += msg + '<br>';
}
function Send(msg) {
    server.trigger('message', { data: msg });
    
}
function GetOnlineUserTimerStart() {
    alertTimerId = setTimeout("GetOnlineUserTimerStart()", 5000);
    ShowOnlineUsers();
}
function GetOnlineUserTimerStop() {
    clearTimeout(alertTimerId);
}
function Onkey(event, id) {
    if (event.keyCode == 13) {
        var id_number = parseInt(id.substr(10));
        var toUser = globalOnlineUserStore[id_number].UserName;
        var message = document.getElementById('txtChatBox' + id_number).value;
        SendMessage(toUser, message, id_number);
        GetOnlineUserTimerStart();
    }
   
};

function ShowTxtChatBox(id) {
    var id_number = parseInt(id.substr(2));

    for (var i = 0; i < globalOnlineUserStore.length; i++) {
        document.getElementById('txtChatBox' + i).setAttribute('style', 'width:214px;display:none');
    }

    document.getElementById('txtChatBox' + id_number).setAttribute('style', 'width:214px;display:block');
    chatUser = globalOnlineUserStore[id_number].UserName;
    GetOnlineUserTimerStop();
    RecentMessages(id_number);
}

function SelectContent(id) {
    var id_number = parseInt(id.substr(9));

    for (var i = 0; i < globalAllContentStore.length; i++) {
        document.getElementById('licontent' + i).setAttribute('style', 'background-color: #E18B6B');
    }

    document.getElementById('licontent' + id_number).setAttribute('style', 'background-color: Red');
    selectedContedName = globalAllContentStore[id_number].ContentName;

    var outputHeader = document.getElementById("container");
    while (outputHeader.firstChild) {
        outputHeader.removeChild(outputHeader.firstChild);
    }
    GetContentPosition();
}

function BuildOnlineUsersBox(arg, context) {

    var values = eval("(" + arg + ")");

    globalOnlineUserStore = values;

    var output = document.getElementById("Console");
    if (values.length == 0) {
    }
    else {
        while (output.firstChild) {
            output.removeChild(output.firstChild);
        }

        var ul = document.createElement("ul");
        ul.setAttribute('class', 'noindent');
        for (var i = 0; i < values.length; i++) {

            var userName = document.createTextNode(values[i].UserName);

            var img = document.createElement('img');
            img.setAttribute('class', 'chatimage');
            img.setAttribute('src', 'Images/karthik.jpg');

            var link = document.createElement("a");
            link.appendChild(userName);

            var txtChatBox = document.createElement('input');
            txtChatBox.setAttribute('type', 'text');
            txtChatBox.setAttribute('id', 'txtChatBox' + i);
            txtChatBox.setAttribute('class', 'txtChatBox');
            txtChatBox.setAttribute('onkeypress', 'Onkey(event, this.id)');
            txtChatBox.setAttribute('onfocus', 'javascript:clearInputValue(&#39;txtChatBox&#39;, &#39;Typehere&#39;)');

            var li = document.createElement("li");
            li.setAttribute('class', 'cursor');
            li.setAttribute('id', 'li' + i);
            li.setAttribute('onclick', 'ShowTxtChatBox(this.id)');

            li.appendChild(img);
            li.appendChild(link);
            li.appendChild(txtChatBox);

            ul.appendChild(li);
        }
        output.appendChild(ul);

    }
}



function SentMessage(id_number) {
    document.getElementById('txtChatBox' + id_number).value = "";
}

function CreatedContent() {
    ShowAllContent();
    document.getElementById('ContentName').value = "";
}

function BuildMessageBox(arg, context) {
    var values = eval("(" + arg + ")");
    ShowElement('MessagesBoxTable');
    globalMessageStore = values;
    var outputHeader = document.getElementById("ChatUserImage");
    while (outputHeader.firstChild) {
        outputHeader.removeChild(outputHeader.firstChild);
    }
    var img = document.createElement('img');
    img.setAttribute('class', 'chatimage');
    img.setAttribute('src', 'Images/karthik.jpg');

    var schatUser = document.createTextNode(chatUser);
    var spanChatUser = document.createElement("span");
    spanChatUser.setAttribute('class', 'normalmessage3');

    spanChatUser.appendChild(schatUser);

    outputHeader.appendChild(img);
    outputHeader.appendChild(spanChatUser);

    var output = document.getElementById("MessagesBox");
    if (values.length == 0) {
    }
    else {
        while (output.firstChild) {
            output.removeChild(output.firstChild);
        }

        var ul = document.createElement("ul");
        ul.setAttribute('class', 'msgbox');
        for (var i = 0; i < values.length; i++) {

            var userName;
            userName = document.createTextNode(values[i].FromUser + ":  ");
//            if (values[i].FromUser == loggedinUser) {
//                userName = document.createTextNode('You :  ');
//            }
            var span1 = document.createElement("span");
            span1.setAttribute('class', 'normalmessage1');
            span1.appendChild(userName);

            var msg = document.createTextNode(values[i].Message);
            var span2 = document.createElement("span");
            span2.setAttribute('class', 'normalmessage2');
            span2.appendChild(msg);

  

            var li = document.createElement("li");
            li.setAttribute('id', 'li' + i);
//            li.appendChild(img);
            li.appendChild(span1);
            li.appendChild(span2);

            ul.appendChild(li);
        }
        output.appendChild(ul);

    }
}



function BuildAllContentBox(arg, context) {

    var values = eval("(" + arg + ")");

    globalAllContentStore = values;

    var output = document.getElementById("AllContent");
    if (values.length == 0) {
    }
    else {
        while (output.firstChild) {
            output.removeChild(output.firstChild);
        }

        for (var i = 0; i < values.length; i++) {

            var ul;      

            var contentName;
            var li = document.createElement("li");
            if (values[i].ContentType == 'Ma') {
                ul =  document.createElement("ul");
                ul.setAttribute('class', 'indent1');
                contentName = document.createTextNode(values[i].ContentName);
                li.setAttribute('class', 'MacroContent indent'); 
            }
            else {
                    ul =  document.createElement("ul");
                    ul.setAttribute('class', 'indent2');
                contentName = document.createTextNode(values[i].ContentName);
                li.setAttribute('class', 'MicroContent indent'); 
            }

            var link = document.createElement("a");
            link.appendChild(contentName);

            li.setAttribute('id', 'licontent' + i);
            li.setAttribute('onclick', 'SelectContent(this.id)');

            li.appendChild(link);
            ul.appendChild(li);
            output.appendChild(ul);
        }
        

    }
}


