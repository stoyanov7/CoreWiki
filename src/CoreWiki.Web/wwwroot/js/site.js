(function() {
    const reformatTimeStamps = function() {
        const timeStamps = document.getElementsByClassName("timeStampValue");

        for (let timeStamp of timeStamps) {
            const currentTimeStamp = timeStamp.getAttribute("data-value");
            const date = new Date(currentTimeStamp);
            timeStamp.textContent = moment(date).format('"MMM Do YY"');
        }
    };

    reformatTimeStamps();
})();