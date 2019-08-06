﻿(function() {
    let reformatTimeStamps = function() {
        let timeStamps = document.getElementsByClassName("timeStampValue");

        for (let timeStamp of timeStamps) {
            let currentTimeStamp = timeStamp.getAttribute("data-value");
            let date = new Date(currentTimeStamp);

            moment.locale(window.navigator.userLanguage || window.navigator.language);
            timeStamp.textContent = moment(date).format('LLL');
        }
    };

    reformatTimeStamps();
})();

function fillInputWithName() {
    $('#inputGroupFile01').on('change', function () {
        var fileName = $(this).val().replace('C:\\fakepath\\', " ");;
        $(this).next('.custom-file-label').html(fileName);
    });
}