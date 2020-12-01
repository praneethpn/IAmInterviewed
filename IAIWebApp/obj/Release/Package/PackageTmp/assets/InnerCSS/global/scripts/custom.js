function onComponentLoad() {
    FormWizard.init();
}

function DatePickerfunc() {

    $('.date').datepicker({
        useCurrent: true,
        format: 'mm/dd/yyyy',
        showButtonPanel: true,
        changeMonth: true,
        changeYear: true,
        defaultDate: new Date(),
        autoclose: true
    });

    //$('.colorpicker').colorpicker();

    // Colorpicker
    if ($.isFunction($.fn.colorpicker)) {
        $(".colorpicker").each(function (i, el) {
            var $this = $(el);
            var opts = {
                //format: attrDefault($this, 'format', false)
            };
            var $nextEle = $this.next();
            var $prevEle = $this.prev();
            var $colorPreview = $this.siblings('.input-group-addon').find('.icon-color-preview');

            $this.colorpicker(opts);

            if ($nextEle.is('.input-group-addon') && $nextEle.has('span')) {
                $nextEle.on('click', function (ev) {
                    ev.preventDefault();
                    $this.colorpicker('show');
                });
            }

            if ($prevEle.is('.input-group-addon') && $prevEle.has('span')) {
                $prevEle.on('click', function (ev) {
                    ev.preventDefault();
                    $this.colorpicker('show');
                });
            }

            if ($colorPreview.length) {
                $this.on('changeColor', function (ev) {

                    $colorPreview.css('background-color', ev.color.toHex());
                });

                if ($this.val()) {
                    $colorPreview.css('background-color', $this.val());
                }
            }
        });
    }
}

function DatePickerfuncAllDates() {
    var setStartDate = new Date();
    setStartDate.setMonth(setStartDate.getMonth() - 3);
    $('.date').datepicker({
        format: 'mm/dd/yyyy',
        showButtonPanel: true,
        changeMonth: true,
        changeYear: true,
        defaultDate: new Date(),
        autoclose: true,
        startDate: "01/01/1900"
    });
}

function DatePickerfuncAllDatesddmm() {
    var setStartDate = new Date();
    setStartDate.setMonth(setStartDate.getMonth() - 3);
    $('.date').datepicker({
        format: 'dd/mm/yyyy',
        showButtonPanel: true,
        changeMonth: true,
        changeYear: true,
        defaultDate: new Date(),
        autoclose: true,
        startDate: "01/01/1900"
    });
}
//Loader Code Starts
var loaderPresent = true;
function manageLoader(action) {

    if (action == null || action == "") {
        hideLoader();
    } else if ($('.loadingWrapper-custom').is(':visible') && loaderPresent == true) {
        //IF Loader already visible then do not repaint it
    } else {
        showLoader();
    }
    /* setTimeout("$('.loadingWrapper').hide();", timer);*/
}

function showLoader() {
    var body = document.body,
    html = document.documentElement;

    var height = Math.max(body.scrollHeight, body.offsetHeight,
                       html.clientHeight, html.scrollHeight, html.offsetHeight);
    $('.loadingWrapper-custom .loaderCntr-custom').height(height);
    $('.loadingWrapper-custom').show();
    loaderPresent = true;
}

function hideLoader() {
    $('.loadingWrapper-custom').hide();
    loaderPresent = false;
}
//Loader Code Ends

//Notification Code Starts
function showNotification(status) {
    $('#notificationBar').show();
    $('#notificationBar').animate({ right: '50px' });
    if (status == 'success') {
        $('#notificationBar').removeClass('error');
        $('#notificationBar').removeClass('warning');
        $('#notificationBar').addClass('success');
    }
    else if (status == 'warning') {
        $('#notificationBar').removeClass('error');
        $('#notificationBar').removeClass('success');
        $('#notificationBar').addClass('warning');
    }
    else {
        $('#notificationBar').removeClass('success');
        $('#notificationBar').removeClass('warning');
        $('#notificationBar').addClass('error');
    }
    setTimeout("setNotificationWidth()", 3000);
    setTimeout('closeNotication()', 3000);
}
function setNotificationWidth() {
    if ($('#notificationBar').children()[1].innerHTML.length > 100) {
        $('#notificationBar').width(700);
    }
    else { $('#notificationBar').width(430); }
}
function closeNotication() {
    $('#notificationBar').animate({ right: '-600px' });
    //$('#notificationBar').hide();
    setTimeout("$('#notificationBar').hide()", 300);
}
//Notification Code Starts
$(document).on('keydown', function (event) {
    if (event.key == "Escape") {
        //alert('Esc key pressed.');
        manageLoader()
    }
});