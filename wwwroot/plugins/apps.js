$(document).ready(function () {
  const menu1 = document.querySelector('#menu-1');
  if (menu1) {
    new Menu(menu1);
  }
});
function startLoading() {
  $.blockUI({
    message: '<div class="spinner-border text-primary" role="status"></div>',
    timeout: 300000,
    css: {
      border: 0,
      color: '#fff',
      padding: 0,
      backgroundColor: 'transparent'
    },
    overlayCSS: {
      backgroundColor: "#fff",
      opacity: 0.8
    }
  });
}

function endLoading() {
  $.unblockUI();
}

function showToast(data) {
  const flag = (data.Success==true) ? undefined : false;
  if (flag) {
    toastr.success(data.Message, 'Message')
  } else {
    toastr.error("ERROR :" + data.Message, 'Message')
  }
  toastr.options = {
    "closeButton": false,
    "debug": true,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-top-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "1000",
    "hideDuration": "1500",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
  }
}

function isConfirm(title, content, confirm, cancel) {
  if (cancel == undefined) {
    cancel = function () {
    };
  }
  $.confirm({
    title: title,
    content: content,
    autoClose: 'cancel|10000',
    buttons: {
      confirm: {
        text: 'Ya',
        btnClass: 'btn-success',
        keys: ['enter'],
        action: confirm
      },
      cancel: {
        text: 'Batal',
        btnClass: 'btn-danger',
        keys: ['esc'],
        action: cancel
      }
    }
  });
}

function RequestAsync(method, url, dataType, data, callback, isLoading, toast) {
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
      if (isLoading) {
        endLoading();
        showToast(xhr);
      }
    },
  }).done(function (data) {
    if (toast) {
      showToast(data);
    }

    if (isLoading) {
      endLoading();
    }
  });
}

function APIRequestAsync(method, url, dataType, data, callback, isLoading, toast, isSaveLog) {
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

    let methodList = ['PUT', 'POST', 'PATCH']
    if (isSaveLog) {
      if (method.contains(methodList)) {
        // exec method log audit

      }
    }
  });
}

function objectifyForm(formArray) {
  var returnArray = {};
  for (var i = 0; i < formArray.length; i++) {
    returnArray[formArray[i]['name']] = formArray[i]['value'];
  }
  return returnArray;
}
