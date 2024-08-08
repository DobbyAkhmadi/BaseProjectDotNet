$(document).ready(async function () {
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
  let token;

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
        responseError(xhr);
      }
    },
  }).done(function (data) {
    formValid()
    if (toast && method !== "GET") {
      showToast(data);
    }
    // log success
    if (isLoading) {
      endLoading();
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

function convertIsArchived(element) {
  // Ensure the element is a jQuery object
  const obj = $(element);
  // Check if the element is checked
  return obj.is(':checked') ? 5 : 1;
}

function validateField(field, helpBlock, errorMessage = '') {
  if (!field.value.trim() || errorMessage) {
    // show error
    $(field).addClass('is-invalid');
    $(helpBlock).removeClass('d-none');
    $(helpBlock)
      .addClass('invalid-tooltip')
      .removeClass('valid-tooltip');
    $(helpBlock).text(errorMessage);
    return false;
  } else {
    // hide error & show valid
    $(field).removeClass('is-invalid');
    $(helpBlock).addClass('d-none');
    $(helpBlock)
      .removeClass('invalid-tooltip')
      .addClass('valid-tooltip');
    $(helpBlock).text('');
    return true;
  }
}

function formValid() {
  // Clear validation states and messages for form controls
  $('.form-control').removeClass('is-invalid');
  $('.help-block')
    .removeClass('invalid-tooltip d-block')
    .addClass('valid-tooltip')
    .text('');
}

function responseError(xhr) {
  switch (xhr.status) {
    // handle bad request
    case 400:
      formValid();
      const errors = xhr.responseJSON;
      for (const [fieldName, messages] of Object.entries(errors)) {
        const fieldElement = document.querySelector(`#${fieldName}`);
        const helpBlock = document.querySelector(`#${fieldName}-help-block`);
        if (fieldElement && helpBlock) {
          validateField(fieldElement, helpBlock, messages[0]); // Display the first error message
        }
      }
      break;
    case 403:
      window.location.href = '/internal/Home/Error?statusCode=403'; // Redirect to unauthorized page
      break;
    case 401:
      window.location.href = '/internal/Home/Error?statusCode=401'; // Redirect to unauthorized page
      break;
    default:
      let data = {
        success: false,
        message: xhr.responseJSON
      }
      showToast(data);
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
