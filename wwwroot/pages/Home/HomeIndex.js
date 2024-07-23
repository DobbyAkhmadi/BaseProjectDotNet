let datatable;
$(document).ready(function () {
    alert("x")
    datatable = new DataTable('#dataTable');
    datatable = $('#dataTable').DataTable({
        pageLength: 10,  // Number of rows per page
        lengthMenu: [10, 25, 50, 100], // Options for rows per page
    });

});
const getRandomInt = (min, max) => Math.floor(Math.random() * (max - min + 1)) + min;

const getRandomItem = (arr) => arr[getRandomInt(0, arr.length - 1)];

const generateRandomData = (numRows) => {
    const names = ['John Doe', 'Jane Smith', 'Alice Johnson', 'Bob Brown'];
    const genders = ['1', '2'];
    const hobbies = ['1', '2', '3', '4'];

    const data = [];
    for (let i = 0; i < numRows; i++) {
        data.push([
            i + 1,
            getRandomItem(names),
            getRandomItem(genders),
            getRandomItem(hobbies),
            getRandomInt(18, 40)
        ]);
    }
    return data;
};
$("#generateButton").on('click', function () {
    const data = generateRandomData(1000);
    datatable.rows.add(data).draw();
});

$("#submitButton").on('click', function () {
    let url = "person/save";
    let form = {
        persons :  arrayTable()
    }

    requestAjaxAsync('POST', url, 'formData', form, async function (response) {
        console.log(response)
    });
});

function arrayTable() {
    const tableData = [];
    const rows = document.querySelectorAll('#dataTable tbody tr');

    rows.forEach(row => {
        const cells = row.querySelectorAll('td');
        const rowData = {
            Id: cells[0]?.textContent.trim() || '',
            Name: cells[1]?.textContent.trim() || '',
            IdGender: cells[2]?.textContent.trim() || '',
            IdHobby: cells[3]?.textContent.trim() || '',
            Age: cells[4]?.textContent.trim() || ''
        };

        tableData.push(rowData);
    });

    return tableData;
}