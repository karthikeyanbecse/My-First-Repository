function toggleFilter(filterLayer, filterUrl) {
    if (document.getElementById) {
        // Modern
        var style2 = document.getElementById(filterLayer).style;

        style2.display = style2.display ? "" : "block";
        document.getElementById(filterLayer).innerHTML = "<center><iframe src='" + filterUrl
		    + "' style='width:750px;height:160px;' frameborder=0 scrolling=no></iframe></center>";
    }
    else if (document.all) {
        // Old IE
        var style2 = document.all[filterLayer].style;
        style2.display = style2.display ? "" : "block";
    }
    else if (document.layers) {
        // NN4
        var style2 = document.layers[filterLayer].style;
        style2.display = style2.display ? "" : "block";
    }
}

function show(id) { document.getElementById(id).style.display = 'block'; }
function hide(id) { document.getElementById(id).style.display = 'none'; }

/* search */
function clearInputValue(inputId, defaultText) {
    var obj = document.getElementById(inputId);

    if (obj.value == defaultText) {
        var color1 = obj.style.color;
        obj.style.color = '#000';

        if (color1 != obj.style.color) {
            obj.value = "";
            obj.style.color = '#000';
        }
    }
}

function setDefaultIfEmpty(inputId, defaultText) {
    var obj = document.getElementById(inputId);

    if (obj.value == "") {
        obj.value = defaultText;
        obj.style.color = '#999';
    }
}

function setDefault(inputId, defaultText) {
    var obj = document.getElementById(inputId);

    obj.value = defaultText;
    obj.style.color = '#999';
}

function submitQuery(inputID, defaultText) {
    var obj = document.getElementById(inputID);

    if (obj.value == defaultText) {
        var color1 = obj.style.color;
        obj.style.color = '#000';

        if (color1 != obj.style.color)
            obj.value = "";
    }
}