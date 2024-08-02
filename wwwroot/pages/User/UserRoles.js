$(document).ready(async function () {
  await initDataTable();
});

var datatable;

function initDataTable() {
  datatable = $('#dataTableRoles').DataTable({
    iDisplayLength: 25,
    processing: true,
    serverSide: true,
    ordering: true,
    responsive: true,
    ajax: {
      url: "/internal/User/Roles/index-post",
      type: 'POST',
      contentType: "application/json",
      async: true,
      headers: headerDefaultParam(),
      data: function (data) {
        data.TypeActive = convertIsDeleted($("#IsDeleted"));
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
      {"data": "name"},
      {"data": "description"},
      {
        "data": "type_active",
        'className': 'text-center',
        'render': function (data, type, item, meta) {
          let action = '-'; // Default value if data is not "1"
          if (data === "1") {
            action = '<span class="badge me-4 bg-label-success">Active</span>';
          } else if (data === "3") {
            action = '<span class="badge me-4 bg-label-secondary">Inactive</span>';
          } else {
            action = '<span class="badge me-4 bg-label-warning">Archived</span>';
          }
          return action;
        }
      },
      {
        'className': 'text-center',
        'sortable': false,
        'render': function (data, type, item, meta) {
          let action = '<div class="action">';
          if (item.type_active == "3") {
            action += '<a title="Detail" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-detail"><i class="ti ti-eye ti-md" style="color: lightslategray;"></i></a>';
            action += '<a title="Restore" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-restore"><i class="ti ti-restore ti-md"></i></a>';
          } else if (item.type_active == "5") {
            action += '<a title="Detail" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-detail"><i class="ti ti-eye ti-md" style="color: lightslategray;"></i></a>';
            action += '<a title="Archived" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill"><i class="ti ti-archive ti-md" style="color: blueviolet;"></i></a>';
          } else {
            action += '<a title="Detail" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-detail"><i class="ti ti-eye ti-md" style="color: lightslategray;"></i></a>';
            action += '<a title="Edit" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-edit"><i class="ti ti-pencil ti-md" style="color: green;"></i></a>';
            action += '<a title="Delete" data-id="' + item.id + '" class="btn btn-icon btn-text-danger waves-effect waves-light rounded-pill item-delete"><i class="ti ti-trash ti-md" style="color: red;"></i></a>';
          }
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
          },
          {
            extend: 'csv',
            text: '<i class="ti ti-file-text me-2" ></i>Csv',
            className: 'dropdown-item',
          },
          {
            extend: 'excel',
            text: '<i class="ti ti-file-spreadsheet me-2"></i>Excel',
            className: 'dropdown-item',
          },
          {
            extend: 'pdf',
            text: '<i class="ti ti-file-code-2 me-2"></i>Pdf',
            className: 'dropdown-item',
          }
        ]
      },
      {
        text: '<i class="ti ti-plus me-0 me-sm-1 ti-xs "></i>' +
          '<span class="d-none d-sm-inline-block add-new-roles">Add New Roles</span>',
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

$(document).on("change", "#IsDeleted", function (e) {
  e.preventDefault();
  datatable.draw();
});

$(document).on("click",".add-new-roles",function (){
  $("#role-modal").modal('show');
});
