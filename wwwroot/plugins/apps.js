function startLoading() {
    $.blockUI({
        message: '<i class="icon ion-load-a spinner mr-2"></i> Loading...',
        overlayCSS: {
            backgroundColor: '#1b2024',
            opacity: 0.8,
            cursor: 'wait'
        },
        css: {
            border: 0,
            color: '#ffffff',
            padding: 0,
            backgroundColor: 'transparent'
        }
    });
    $('.blockUI').css({
        'z-index': 9999999
    });
}

function endLoading() {
    $('.blockUI').remove();
}

function showToast(status, toast, data) {
    let icon = 'success';
    let heading = "Success";
    let msg = (data.message === undefined ? data.Message : data.message);
    let stat = (data.success === undefined ? data.Success : data.success);

    if (status === "error") {
        heading = "Error";
        msg = "Harap cek kembali inputan anda";
        icon = 'error';
    }

    if (stat === false) {
        heading = "Error";
        icon = 'error';
    }

    if (stringIsNullOrEmpty(msg)) {
        toast = false;
    }

    if (toast) {
        $.toast({
            heading: heading,
            text: msg,
            showHideTransition: 'slide',
            icon: icon,
            position: 'top-right',
        })
    }

    function stringIsNullOrEmpty(str) {
        return typeof str == 'undefined' || !str || str.length === 0 || str === "" || !/[^\s]/.test(str) || /^\s*$/.test(str) || str.replace(/\s/g, "") === "";
    }
}

function requestAjaxAsync(method, url, dataType, data, callback, isLoading, toast) {
    $.ajax({
        type: method,
        url: url,
        data: data,
        dataType: dataType,
        async: true,
        headers: {'dataType': dataType},
        beforeSend: function () {
            if (isLoading) {
                startLoading();
            }
        },
        success: callback,
        error: function (xhr) {
            showToast("error", true, xhr);
        },
    }).done(function (data) {
        if (toast) {
            showToast("success", true, data);
        }

        if (isLoading) {
            endLoading();
        }
    });
}

function APIRequestAjaxAsync(method, url, dataType, data, callback, isLoading, toast) {
    $.ajax({
        type: method,
        url: url,
        data: data,
        dataType: dataType,
        async: true,
        headers: {'dataType': dataType},
        beforeSend: function () {
            if (isLoading) {
                startLoading();
            }
        },
        success: callback,
        error: function (xhr) {
            showToast("error", true, xhr);
        },
    }).done(function (data) {
        if (toast) {
            showToast("success", true, data);
        }

        if (isLoading) {
            endLoading();
        }
    });
}