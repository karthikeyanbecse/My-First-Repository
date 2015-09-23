function update(group, activeAnchor) {
    var topLeft = group.getChild("topLeft");
    var topRight = group.getChild("topRight");
    var bottomRight = group.getChild("bottomRight");
    var bottomLeft = group.getChild("bottomLeft");
    var image = group.getChild("image");

    // update anchor positions
    switch (activeAnchor.name) {
        case "topLeft":
            topRight.y = activeAnchor.y;
            bottomLeft.x = activeAnchor.x;
            break;
        case "topRight":
            topLeft.y = activeAnchor.y;
            bottomRight.x = activeAnchor.x;
            break;
        case "bottomRight":
            bottomLeft.y = activeAnchor.y;
            topRight.x = activeAnchor.x;
            break;
        case "bottomLeft":
            bottomRight.y = activeAnchor.y;
            topLeft.x = activeAnchor.x;
            break;
    }

    pos.x = topLeft.x;
    pos.y = topLeft.y;
    pos.width = topRight.x - topLeft.x;
    pos.height = bottomLeft.y - topLeft.y;
    image.setPosition(topLeft.x, topLeft.y);
    image.setSize(topRight.x - topLeft.x, bottomLeft.y - topLeft.y);
}
function addAnchor(group, x, y, name) {
    var stage = group.getStage();
    var layer = group.getLayer();

    var anchor = new Kinetic.Circle({
        x: x,
        y: y,
        stroke: "#666",
        fill: "#ddd",
        strokeWidth: 2,
        radius: 8,
        name: name,
        draggable: true
    });

    anchor.on("dragmove", function () {
        update(group, this);
    });
    anchor.on("mousedown", function () {
        group.draggable(false);
        this.moveToTop();
    });
    anchor.on("dragend", function () {
        group.draggable(true);
    });
    // add hover styling
    anchor.on("mouseover", function () {
        var layer = this.getLayer();
        document.body.style.cursor = "pointer";
        this.setStrokeWidth(4);
        layer.draw();
    });
    anchor.on("mouseout", function () {
        var layer = this.getLayer();
        document.body.style.cursor = "default";
        this.setStrokeWidth(2);
        layer.draw();
    });

    group.add(anchor);
}

 
function loadImages1(sources, pos, callback) {
    var images = {};
    var loadedImages = 0;
    var numImages = 0;
    for (var src in sources) {
        numImages++;
    }
    for (var src in sources) {
        images[src] = new Image();
        images[src].onload = function () {
            if (++loadedImages >= numImages) {
                callback(images,pos);
            }
        };
        images[src].src = sources[src];
    }
}
function loadImages(sources, callback) {
    var images = {};
    var loadedImages = 0;
    var numImages = 0;
    for (var src in sources) {
        numImages++;
    }
    for (var src in sources) {
        images[src] = new Image();
        images[src].onload = function () {
            if (++loadedImages >= numImages) {
                callback(images);
            }
        };
        images[src].src = sources[src];
    }
}
function initStage(images) {
    var stage = new Kinetic.Stage({
        container: "container",
//        width: 578,
//        height: 400
        width: 778,
        height: 600
    });
    var darthVaderGroup = new Kinetic.Group({
        x: 270,
        y: 100,
        draggable: true
    });
    var yodaGroup = new Kinetic.Group({
        x: 100,
        y: 110,
        draggable: true
    });
    var layer = new Kinetic.Layer();

    /*
    * go ahead and add the groups
    * to the layer and the layer to the
    * stage so that the groups have knowledge
    * of its layer and stage
    */
    layer.add(darthVaderGroup);
    layer.add(yodaGroup);
    stage.add(layer);

    // darth vader
    var darthVaderImg = new Kinetic.Image({
        x: 0,
        y: 0,
        image: images.darthVader,
        width: 200,
        height: 138,
        name: "image"
    });

    darthVaderGroup.add(darthVaderImg);
    addAnchor(darthVaderGroup, 0, 0, "topLeft");
    addAnchor(darthVaderGroup, 200, 0, "topRight");
    addAnchor(darthVaderGroup, 200, 138, "bottomRight");
    addAnchor(darthVaderGroup, 0, 138, "bottomLeft");

    darthVaderGroup.on("dragstart", function () {
        this.moveToTop();
    });
    // yoda
    var yodaImg = new Kinetic.Image({
        x: 0,
        y: 0,
        image: images.yoda,
        width: 93,
        height: 104,
        name: "image"
    });

    yodaGroup.add(yodaImg);
    addAnchor(yodaGroup, 0, 0, "topLeft");
    addAnchor(yodaGroup, 93, 0, "topRight");
    addAnchor(yodaGroup, 93, 104, "bottomRight");
    addAnchor(yodaGroup, 0, 104, "bottomLeft");

    yodaGroup.on("dragstart", function () {
        this.moveToTop();
    });

    stage.draw();
}


function initStage1(images, pos) {
    var stage = new Kinetic.Stage({
        container: "container",
        width: 578,
        height: 400
    });
    var darthVaderGroup = new Kinetic.Group({
        x: 270,
        y: 100,
        draggable: true
    });
    var layer = new Kinetic.Layer();

   
    layer.add(darthVaderGroup);
    stage.add(layer);

    // darth vader
    var darthVaderImg = new Kinetic.Image({
        x: pos.x,
        y: pos.y,
        image: images.name,
        width: pos.width,
        height: pos.height,
        name: "image"
    });

    darthVaderGroup.add(darthVaderImg);
//    addAnchor(darthVaderGroup, pos.x, pos.y, "topLeft");
//    addAnchor(darthVaderGroup, pos.width, pos.y, "topRight");
//    addAnchor(darthVaderGroup, pos.width, pos.height, "bottomRight");
//    addAnchor(darthVaderGroup, pos.x, pos.height, "bottomLeft");
    addAnchor(darthVaderGroup, 0, 0, "topLeft");
    addAnchor(darthVaderGroup, 200, 0, "topRight");
    addAnchor(darthVaderGroup, 200, 138, "bottomRight");
    addAnchor(darthVaderGroup, 0, 138, "bottomLeft");

    darthVaderGroup.on("dragstart", function () {
        this.moveToTop();
    });
  

    stage.draw();
}

//window.onload = function () {
//    var sources = {
//        darthVader: "darth-vader.jpg",
//        yoda: "yoda.jpg"
//    };
//    loadImages(sources, initStage);
//};


