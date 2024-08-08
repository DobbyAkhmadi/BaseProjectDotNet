$(document).ready(async function () {

});

$(document).on("click", ".btnLogin", function () {
  let url = "/login-post"
  let form = {
    username: $("#username").val(),
    password: $("#password").val()
  }
  RequestAsync("post", url, "json", form, function (response) {
    if (response.success) {
      window.location = '/internal/home'
    }
  }, true, true);
});
