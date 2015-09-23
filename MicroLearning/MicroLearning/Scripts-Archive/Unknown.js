function pareseDate(varDateString) {

    try {
        var myDate = new Date(parseInt(varDateString.replace(/\/+Date\(([\d+-]+)\)\/+/, '$1')));
        return myDate;
    }
    catch (err) {
        //writeMessage('parseStringToDateTime err',err);
    }

}