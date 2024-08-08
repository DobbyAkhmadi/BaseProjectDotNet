$(document).ready(async function () {
  await initDataTable();
});

var datatable;

function initDataTable() {
  datatable = $('#dataTableAudit').DataTable({
    iDisplayLength: 25,
    processing: true,
    serverSide: true,
    ordering: true,
    responsive: true,
    ajax: {
      url: "/internal/Audit/index-post",
      type: 'POST',
      contentType: "application/json",
      async: true,
      headers: headerDefaultParam(),
      data: function (data) {
        data.TypeActive = convertIsArchived($("#IsDeleted"));
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
      {"data": "user_name"},
      {"data": "role_name"},
      {
        "data": "audit_type",
        'className': 'text-center',
        'render': function (data, type, item, meta) {
          let action = "<div class='card-action'>";

          // 1 Log | 2 Auth
          if (data == "1") {
            action += "<span class='badge rounded-pill bg-label-success'>LOG</span>";
          } else if (data == "2") {
            action += "<span class='badge rounded-pill bg-label-primary'>AUTH</span>";
          }
          action += "</div>";

          return action;
        }
      },
      {"data": "remote_ip"},
      {"data": "action"},
      {"data": "function_name"},
      {"data": "message"},
      {"data": "created_date"},
      {
        'className': 'text-center',
        'sortable': false,
        'render': function (data, type, item, meta) {
          let action = '<div class="action-index">';
          if (item.type_active == "5") {
            action += '<a title="Detail" data-id="' + item.id + '" class="btn btn-icon btn-text-success  waves-effect waves-light rounded-pill item-detail"><i class="ti ti-eye ti-md" style="color: green;"></i></a>';
            action += '<a title="Restore" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-restore"><i class="ti ti-restore ti-md"></i></a>';
          } else {
            action += '<a title="Detail" data-id="' + item.id + '" class="btn btn-icon btn-text-success  waves-effect waves-light rounded-pill item-detail"><i class="ti ti-eye ti-md" style="color: green;"></i></a>';
            action += '<a title="Archived" data-id="' + item.id + '" class="btn btn-icon btn-text-success waves-effect waves-light rounded-pill item-archive"><i class="ti ti-archive ti-md" style="color: blueviolet;"></i></a>';
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
    buttons: [
      {
        extend: 'collection',
        className: 'btn btn-label-secondary dropdown-toggle mx-4 waves-effect waves-light',
        text: '<i class="ti ti-upload me-2 ti-xs"></i>Export',
        buttons: [
          {
            extend: 'print',
            text: '<i class="ti ti-printer me-2" ></i>Print',
            className: 'dropdown-item'
          },
          {
            extend: 'csv',
            text: '<i class="ti ti-file-text me-2" ></i>Csv',
            className: 'dropdown-item'
          },
          {
            extend: 'excel',
            text: '<i class="ti ti-file-spreadsheet me-2"></i>Excel',
            className: 'dropdown-item'
          },
          {
            extend: 'pdf',
            text: '<i class="ti ti-file-code-2 me-2"></i>Pdf',
            className: 'dropdown-item'
          }
        ]
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
    'id="IsDeleted" /> Data Archived' +
    '</label>');
}

$(document).on("click", ".add-new", function () {
  datatable.draw();
});


$("#dataTableAudit").on("click", ".item-archive", function (e) {
  e.preventDefault();
  let url = "/internal/Audit/archive"
  let dataId = $(this).data('id');
  let form = {
    id: dataId
  }

  confirm = function () {
    RequestAsync("POST", url, "json", form, function (response) {
      if (response.success == true) {
        datatable.draw();
      }
    }, true, true);
  }
  isConfirm('Archive Confirmation', 'Are you sure want to <b>archive</b> this data ?', confirm)
});

$(document).on("change", "#IsDeleted", function (e) {
  e.preventDefault();
  datatable.draw();
});


$("#dataTableAudit").on("click", ".item-restore", function (e) {
  e.preventDefault();
  let url = "/internal/Audit/restore"
  let dataId = $(this).data('id');
  let form = {
    id: dataId
  }

  confirm = function () {
    RequestAsync("POST", url, "json", form, function (response) {
      if (response.success == true) {
        datatable.draw();
      }
    }, true, true);
  }
  isConfirm('Restore Confirmation', 'Are you sure want to <b>restore</b> this data ?', confirm)
});

$("#dataTableAudit").on("click", ".item-detail", function (e) {
  e.preventDefault();
  let url = "/internal/Audit/detail"
  let dataId = $(this).data('id');
  let form = {
    id: dataId
  }
  RequestAsync("GET", url, "json", form, function (response) {
    let payload = response.payload;
    console.log(payload)
    if (response.success == true) {
      for (const [key, value] of Object.entries(payload)) {
        $("." + key).text(value ? value : "-");
      }
      $("#audit-modal-detail").modal('show');
    }
  }, true, true);
});
