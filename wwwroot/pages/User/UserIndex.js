$(document).ready(async function () {
  await initDataTable();
});

var datatable;

function initDataTable() {
  datatable = $('#dataTableUser').DataTable({
    iDisplayLength: 25,
    processing: true,
    serverSide: true,
    ordering: true,
    responsive: true,
    ajax: {
      url: "/internal/User/index-post",
      type: 'POST',
      contentType: "application/json",
      async: true,
      data: function (data) {
        data.columns.pop();
        return JSON.stringify(data);
      },
      ajaxSend: function () {
        startLoading();
      },
      ajaxComplete: function () {
        endLoading();
      },
      error: function (xhr) {
        endLoading();
        $('#datatable_processing').css('display', 'none');
        $('#datatable').append('<tbody class="datatables_empty"><tr><th colspan="11">' + xhr.statusText + '</th></tr></tbody>');
      }
    },
    columns: [
      {"data": "full_name"},
       {"data": "user_name"},
       {"data": "role_name"},
       {"data": "email"},
       {"data": "type_active"},
      {
        'className': 'text-center',
        'sortable': false,
        'render': function (data, type, item, meta) {
          // console.log(item)
          let action = '<div class="action">';
          action += '<a title="Detail" class="btn btn-icon btn-text-success  waves-effect waves-light rounded-pill"><i class="ti ti-eye ti-md" style="color: lightslategray;"></i></a>';
          // action += '<a title="Archived" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill"><i class="ti ti-archive ti-md" style="color: blueviolet;"></i></a>';
          action += '<a title="Edit" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill"><i class="ti ti-pencil ti-md" style="color: green;"></i></a>';
          action += '<a title="Delete" class="btn btn-icon btn-text-danger waves-effect waves-light rounded-pill"><i class="ti ti-trash ti-md" style="color: red;"></i></a>';
          //  action += '<a title="Restore" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill"><i class="ti ti-restore ti-md"></i></a>';
          action += '</div>';
          return action;
        }
      }
    ],
    columnDefs: [
      {
        defaultContent: "-",
        targets: "_all"
      }
    ],
    dom:
      '<"row"' +
      '<"col-md-4 d-flex align-items-center"l>' +
      '<"col-md-8 d-flex justify-content-end align-items-center"fB>' +
      '>' +
      't' +
      '<"row"' +
      '<"col-md-4"i>' +
      '<"col-md-4"p>' +
      '>',
    language: {
      sLengthMenu: '_MENU_',
      search: '',
      searchPlaceholder: 'Search',
      paginate: {
        next: '<i class="ti ti-chevron-right ti-sm"></i>',
        previous: '<i class="ti ti-chevron-left ti-sm"></i>'
      }
    },
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

  // Apply Select2 to the length menu
  $('.dataTables_length select').select2({
    minimumResultsForSearch: Infinity // Hide search box in Select2
  });

  //Add a checkbox after the Select2 element
  $('.dataTables_length').append(
    '<label>' +
    '<input class="form-check-input" ' +
    'style="margin-left: 25%" ' +
    'type="checkbox" ' +
    'id="IsDeleted" /> Data Terhapus' +
    '</label>');
}

$(document).on("click", ".add-new-role", function () {
  $("#modal-role").modal('show');
});
