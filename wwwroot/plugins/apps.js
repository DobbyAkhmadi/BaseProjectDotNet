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
  console.log(data)
  const flag = (data.success !== undefined) && (data.message !== undefined);

  if (flag) {
    if (data.success) {
      toastr.success(data.message, 'Message');
    } else {
      toastr.error("ERROR: " + data.message, 'Message');
    }
  } else {
    toastr.error("ERROR: Undefined data structure", 'Message');
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
        text: 'Yes',
        btnClass: 'btn-success',
        keys: ['enter'],
        action: confirm
      },
      cancel: {
        text: 'Cancel',
        btnClass: 'btn-danger',
        keys: ['esc'],
        action: cancel
      }
    }
  });
}

function headerDefaultParam() {
  // Get the JWT token from the cookie
  let token = getCookie('JwtToken');

  // Return the headers object
  return {
    "Authorization": token ? "Bearer " + token : "", // Include "Bearer " prefix only if token is present
    "Content-Type": "application/json"
  };
}

function RequestAsync(method, url, dataType, data, callback, isLoading, toast) {
  $.ajax({
    type: method,
    url: url,
    data: data,
    dataType: dataType,
    async: true,
  //  headers: headerDefaultParam(),
    beforeSend: function () {
      if (isLoading) {
        startLoading();
      }
    },
    success: callback,
    error: function (xhr) {
      if (isLoading) {
        endLoading();
        // log failed
        responseError(xhr);
      }
    },
  }).done(function (data) {
    if (toast) {
      showToast(data);
    }
    // log success

    if (isLoading) {
      endLoading();
    }
  });
}

function RequestAsyncLogin(method, url, dataType, data, callback, isLoading, toast) {
  $.ajax({
    type: method,
    url: url,
    data: data,
    dataType: dataType,
    async: true,
    beforeSend: function () {
      if (isLoading) {
        startLoading();
      }
    },
    success: callback,
    error: function (xhr) {
      if (isLoading) {
        endLoading();
        // log failed
        responseError(xhr);
      }
    },
  }).done(function (data) {
    if (toast) {
      showToast(data);
    }
    // log success

    if (isLoading) {
      endLoading();
    }
  });
}

function APIRequestAsync(method, url, dataType, data, callback, isLoading, toast, isSaveLog) {
  let methodList = ['PUT', 'POST', 'PATCH', 'DELETE']
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
        // log failed
        if (isSaveLog) {
          if (method.contains(methodList)) {
            // exec method log audit

          }
        }
      }
    },
  }).done(function (data) {
    if (toast) {
      showToast(data);
    }

    if (isLoading) {
      endLoading();
    }

    if (isSaveLog) {
      if (method.contains(methodList)) {
        // exec method log audit

      }
    }
  });
}

function formToJson(formElement) {
  const formData = new FormData(formElement);
  return Object.fromEntries(formData.entries());
}

function convertIsDeleted(element) {
  // Ensure the element is a jQuery object
  const obj = $(element);
  // Check if the element is checked
  return obj.is(':checked') ? 3 : 1;
}

function responseError(xhr) {
  switch (xhr.status) {
    // case 400:
    //     window.location.href = '/internal/Home/Error?statusCode=400'; // Redirect to unauthorized page
    //   break;
    case 401:
      window.location.href = '/internal/Home/Error?statusCode=401'; // Redirect to unauthorized page
      break;
    default:
      window.location.href = '/internal/Home/Error?statusCode=500'; // Redirect to unauthorized page
      break;
  }
}

function isAlert(title, content) {
  $.alert({
    title: title,
    content: content,
    buttons: {
      ok: {
        text: 'Yes',
        btnClass: 'btn-primary',
        keys: ['enter'],
      }
    }
  });
}


// Helper function to get a cookie value by name
function getCookie(name) {
  let value = "; " + document.cookie;
  let parts = value.split("; " + name + "=");
  if (parts.length === 2) return parts.pop().split(";").shift();
}
