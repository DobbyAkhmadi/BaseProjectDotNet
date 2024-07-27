let datatable;
$(document).ready(function () {
  var var_datatable = $('#dataTable');

  if (var_datatable.length) {
    datatable = var_datatable.DataTable({
      // processing: true,
      // serverSide: true,
      // destroy: true,
      // ordering: true,
      // ajax: {
      //   "url": "http://localhost:8080/list",
      //   "type": 'POST',
      //   "contentType": "application/json",
      //   "datatype": "json",
      //   data: function (data) {
      //
      //     return JSON.stringify(data)
      //   },
      //   error: function (xhr) {
      //     $('#datatable_processing').css('display', 'none');
      //     $('#datatable').append('<tbody class="datatables_empty"><tr><th colspan="11">' + xhr.statusText + '</th></tr></tbody>');
      //   }
      // },
      columns: [
        {data: 'user_name'},
        {data: 'roles_name'},
        {data: 'salary'},
        {data: 'action'},
        {data: 'error_message'},
        {data: 'old_model'},
        {data: 'new_model'},
        {data: 'remote_ip'},
        {data: 'latency'},
        {data: 'timestamp'},
        {
          'className': 'text-center',
          'render': function (data, type, item, meta) {
            console.log(item)
            let action = '<div class="action">';
              action += '<a title="Detail" class="btn btn-icon btn-text-secondary  waves-effect waves-light rounded-pill"><i class="ti ti-eye ti-md"></i></a>';
              action += '<a title="Edit" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill"><i class="ti ti-pencil ti-md"></i></a>';
              action += '<a title="Delete" class="btn btn-icon btn-text-danger waves-effect waves-light rounded-pill"><i class="ti ti-trash ti-md"></i></a>';
              action += '<a title="Restore" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill"><i class="ti ti-restore ti-md"></i></a>';
              action += '</div>';
            return action;
          }
        }
      ],
      columnDefs: [
        {responsivePriority: 1, targets: 0},
        {responsivePriority: 2, targets: -1}
      ],
      // add button
      dom: '<"card-header flex-column flex-md-row"<"head-label text-center"><"dt-action-buttons text-end pt-3 pt-md-0"B>><"row"<"col-sm-12 col-md-6"l><"col-sm-12 col-md-6 d-flex justify-content-center justify-content-md-end"f>>t<"row"<"col-sm-12 col-md-6"i><"col-sm-12 col-md-6"p>>',
      displayLength: 10,
      lengthMenu: [10, 25, 50, 75, 100],
      // Buttons with Dropdown
      buttons: [
        {
          extend: 'collection',
          className: 'btn btn-label-secondary dropdown-toggle mx-4 waves-effect waves-light',
          text: '<i class="ti ti-upload me-2 ti-xs"></i>Export',
          buttons: [
            {
              extend: 'print',
              text: '<i class="ti ti-printer me-2" ></i>Print',
              className: 'dropdown-item',
              exportOptions: {
                columns: [1, 2, 3, 4, 5],
                // prevent avatar to be print
                format: {
                  body: function (inner, coldex, rowdex) {
                    if (inner.length <= 0) return inner;
                    var el = $.parseHTML(inner);
                    var result = '';
                    $.each(el, function (index, item) {
                      if (item.classList !== undefined && item.classList.contains('user-name')) {
                        result = result + item.lastChild.firstChild.textContent;
                      } else if (item.innerText === undefined) {
                        result = result + item.textContent;
                      } else result = result + item.innerText;
                    });
                    return result;
                  }
                }
              },
              customize: function (win) {
                //customize print view for dark
                $(win.document.body)
                  .css('color', headingColor)
                  .css('border-color', borderColor)
                  .css('background-color', bodyBg);
                $(win.document.body)
                  .find('table')
                  .addClass('compact')
                  .css('color', 'inherit')
                  .css('border-color', 'inherit')
                  .css('background-color', 'inherit');
              }
            },
            {
              extend: 'csv',
              text: '<i class="ti ti-file-text me-2" ></i>Csv',
              className: 'dropdown-item',
              exportOptions: {
                columns: [1, 2, 3, 4, 5],
                // prevent avatar to be display
                format: {
                  body: function (inner, coldex, rowdex) {
                    if (inner.length <= 0) return inner;
                    var el = $.parseHTML(inner);
                    var result = '';
                    $.each(el, function (index, item) {
                      if (item.classList !== undefined && item.classList.contains('user-name')) {
                        result = result + item.lastChild.firstChild.textContent;
                      } else if (item.innerText === undefined) {
                        result = result + item.textContent;
                      } else result = result + item.innerText;
                    });
                    return result;
                  }
                }
              }
            },
            {
              extend: 'excel',
              text: '<i class="ti ti-file-spreadsheet me-2"></i>Excel',
              className: 'dropdown-item',
              exportOptions: {
                columns: [1, 2, 3, 4, 5],
                // prevent avatar to be display
                format: {
                  body: function (inner, coldex, rowdex) {
                    if (inner.length <= 0) return inner;
                    var el = $.parseHTML(inner);
                    var result = '';
                    $.each(el, function (index, item) {
                      if (item.classList !== undefined && item.classList.contains('user-name')) {
                        result = result + item.lastChild.firstChild.textContent;
                      } else if (item.innerText === undefined) {
                        result = result + item.textContent;
                      } else result = result + item.innerText;
                    });
                    return result;
                  }
                }
              }
            },
            {
              extend: 'pdf',
              text: '<i class="ti ti-file-code-2 me-2"></i>Pdf',
              className: 'dropdown-item',
              exportOptions: {
                columns: [1, 2, 3, 4, 5],
                // prevent avatar to be display
                format: {
                  body: function (inner, coldex, rowdex) {
                    if (inner.length <= 0) return inner;
                    var el = $.parseHTML(inner);
                    var result = '';
                    $.each(el, function (index, item) {
                      if (item.classList !== undefined && item.classList.contains('user-name')) {
                        result = result + item.lastChild.firstChild.textContent;
                      } else if (item.innerText === undefined) {
                        result = result + item.textContent;
                      } else result = result + item.innerText;
                    });
                    return result;
                  }
                }
              }
            }
          ]
        },
        {
          text: '<i class="ti ti-plus me-0 me-sm-1 ti-xs"></i><span class="d-none d-sm-inline-block">Add New User</span>',
          className: 'add-new btn btn-primary waves-effect waves-light',
          attr: {
            'data-bs-toggle': 'offcanvas',
            'data-bs-target': '#offcanvasAddUser'
          }
        }
      ]
    });

    // Add a checkbox after the Select2 element
    $('.dataTables_length').append(
      '<label>' +
        '<input class="form-check-input" ' +
        'style="margin-left: 15px" ' +
        'type="checkbox" ' +
        'id="IsDeleted" /> Data Terhapus' +
      '</label>');
  }
});

$(document).on("click",".create-new",function (){
  alert("clicked")
});

$("#btn-click").on("click", async function () {
  let form ={
    Success :true,
    Message : "xxxxxxx"
  }
  confirm = function () {
    showToast(form)
  }
  isConfirm('Delete Confirmation', 'Are you sure want to <b>delete</b> this data ?', confirm)

});

$('#TypeTransport').last().select2({
  width: '100%',
  placeholder: 'select',
  minimumInputLength: 0
  //,
  // ajax: {
  //   url: 'master/TypeTransportSelect2',
  //   dataType: 'json',
  //   delay: 0,
  //   cache: true,
  //   data: function (param) {
  //     return {
  //       q: param,
  //     };
  //   },
  //   processResults: function (data, params) {
  //     var output = [];
  //     var results = data.Payload;
  //     if (results) {
  //       $.each(results, function (index) {
  //         output.push({
  //           'id': results[index]['id'],
  //           'text': results[index]['text']
  //         });
  //       });
  //     }
  //     return {
  //       results: output
  //     };
  //   }
  // }
});
