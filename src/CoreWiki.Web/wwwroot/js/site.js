(function() {
    let reformatTimeStamps = function() {
        let timeStamps = document.getElementsByClassName("timeStampValue");

        for (let timeStamp of timeStamps) {
            let currentTimeStamp = timeStamp.getAttribute("data-value");
            let date = new Date(currentTimeStamp);
            timeStamp.textContent = moment(date).format('"MMM Do YY"');
        }
    };

    reformatTimeStamps();
})();