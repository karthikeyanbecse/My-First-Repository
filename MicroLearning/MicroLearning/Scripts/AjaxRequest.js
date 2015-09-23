function AjaxRequest(baseurl, type, callbackResponse, parameterString) {
    this.BaseURL = baseurl;
    this.Type = type;
    this.Callback = callbackResponse;
    this.createXmlRequestObject();
    this.ParemeterString = parameterString;
}

AjaxRequest.prototype.createXmlRequestObject = function () {
    if (window.ActiveXObject) { // INTERNET EXPLORER
        try {
            this.xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
        } catch (e) {
            this.xmlHttp = false;
        }
    }
    else { // OTHER BROWSERS
        try {
            this.xmlHttp = new XMLHttpRequest()
        } catch (f) {
            this.xmlHttp = false;
        }
    }

    if (!this.xmlHttp) { // RETURN THE OBJECT OR DISPLAY ERROR
        alert('there was an error creating the xmlhttp object');
    } else {
        //return this.xmlhttp;
    }
}

AjaxRequest.prototype.MakeRequest = function () {
    try {

        // PROCEED ONLY IF OBJECT IS NOT BUSY       
        if (this.xmlHttp.readyState === 4 || this.xmlHttp.readyState === 0) {

            // EXECUTE THE PAGE ON THE SERVER AND PASS QUERYSTRING
            this.xmlHttp.open(this.Type, this.BaseURL, false);

            var that = this;
            // DEFINE METHOD TO HANDLE THE RESPONSE
            this.xmlHttp.onreadystatechange = function () {
                try {

                    // MOVE FORWARD IF TRANSACTION COMPLETE
                    //                    alert(that.xmlHttp.readyState);
                    if (that.xmlHttp.readyState == 4) {
                        //                        alert(that.xmlHttp.status);
                        // STATUS OF 200 INDICATES COMPLETED CORRECTLY
                        if (that.xmlHttp.status == 200) {

                            // WILL HOLD THE XML DOCUMENT
                            var xmldoc;
                            if (window.ActiveXObject) { // INTERNET EXPLORER
                                xmldoc = new ActiveXObject("Microsoft.XMLDOM");
                                xmldoc.async = "false";
                                that.Callback(that.xmlHttp.responseText);
                                //                                alert(that.xmlHttp.responseText);
                            }
                            else { // OTHER BROWSERS
                                //writeMessage("MakeRequest", that.xmlHttp.responseText);
                                that.Callback(that.xmlHttp.responseText);
                                //                                alert(that.xmlHttp.responseText);
                            }
                        }
                    }
                }
                catch (e)
                { alert(e) }
            }

            switch (this.Type) {
                case "GET":
                    //this.xmlHttp.setRequestHeader("Content-type", "application/json");
                    // MAKE CALL
                    this.xmlHttp.send(this.BaseURL);
                    break;
                case "POST":
                    this.xmlHttp.setRequestHeader("Content-type", "application/json");
                    this.xmlHttp.send(this.ParemeterString)
            }

        }
        else {
            // IF CONNECTION IS BUSY, WAIT AND RETRY
            //            setTimeout('GetAllAppsService', 5000);
        }
    } catch (e) {
        alert(e);
    }

}



AjaxRequest.prototype.MakeFileRequest = function (file) {
    try {

        // PROCEED ONLY IF OBJECT IS NOT BUSY       
        if (this.xmlHttp.readyState === 4 || this.xmlHttp.readyState === 0) {

            // EXECUTE THE PAGE ON THE SERVER AND PASS QUERYSTRING
            this.xmlHttp.open(this.Type, this.BaseURL, false);

            var that = this;
            // DEFINE METHOD TO HANDLE THE RESPONSE
            this.xmlHttp.onreadystatechange = function () {
                try {

                    // MOVE FORWARD IF TRANSACTION COMPLETE
                    //                    alert(that.xmlHttp.readyState);
                    if (that.xmlHttp.readyState == 4) {
                        //                        alert(that.xmlHttp.status);
                        // STATUS OF 200 INDICATES COMPLETED CORRECTLY
                        if (that.xmlHttp.status == 200) {

                            // WILL HOLD THE XML DOCUMENT
                            var xmldoc;
                            if (window.ActiveXObject) { // INTERNET EXPLORER
                                xmldoc = new ActiveXObject("Microsoft.XMLDOM");
                                xmldoc.async = "false";
                                that.Callback(that.xmlHttp.responseText);
                                //                                alert(that.xmlHttp.responseText);
                            }
                            else { // OTHER BROWSERS
                                //writeMessage("MakeRequest", that.xmlHttp.responseText);
                                that.Callback(that.xmlHttp.responseText);
                                //                                alert(that.xmlHttp.responseText);
                            }
                        }
                    }
                }
                catch (e)
                { alert(e) }
            }

            switch (this.Type) {
                case "GET":
                    //this.xmlHttp.setRequestHeader("Content-type", "application/json");
                    // MAKE CALL
                    this.xmlHttp.send(this.BaseURL);
                    break;
                case "POST":
                    this.xmlHttp.setRequestHeader("Cache-Control", "no-cache");
                    this.xmlHttp.setRequestHeader("X-Requested-With", "XMLHttpRequest");
                    this.xmlHttp.setRequestHeader("X-File-Name", file.name);
//                    this.xmlHttp.setRequestHeader("Content-type", "application/json");
                    this.xmlHttp.send(file)
            }

        }
        else {
            // IF CONNECTION IS BUSY, WAIT AND RETRY
            //            setTimeout('GetAllAppsService', 5000);
        }
    } catch (e) {
        alert(e);
    }

}
