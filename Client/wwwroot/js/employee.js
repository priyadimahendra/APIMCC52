$(document).ready(function () {
    var table = $('#myTable').DataTable({
        dom: '<"html5buttons">Blfrtip',
        language: {
            buttons: {
                colvis: 'Show / Hide', // label button show / hide
                colvisRestore: "Reset Kolom" //lael untuk reset kolom ke default
            }
        },
        buttons: [
            { extend: 'colvis', postfixButtons: ['colvisRestore'] },
            { extend: 'copy' },
            { extend: 'csv', title: 'Daftar Karyawan' },
            { extend: 'pdf', title: 'Daftar Karyawan', orientation:'landscape' },
            { extend: 'excel', title: 'Daftar Karyawan' },
            { extend: 'print', title: 'Daftar Karyawan' },
        ],

        'ajax': {
            url: "/employee/showprofile/",
            dataType: "json",
            dataSrc: ""
        },

        "columns": [
            {
                "data": "nik"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    let name = row["firstName"] +" " + row["lastName"]
                    return name;
                }
            },
            {
                "data": "email"
            },
            {
                "data": "salary"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    let namber = row["phoneNumber"].substring(0, 2);
                    let namber2 = row["phoneNumber"].substring(1);
                    if (namber == "08") {
                        let indo = "+62 ";
                        return indo + namber2;
                    }
                }
            },
            {
                "data": "birthDate"
            },
            {
                "data": "degree"
            },
            {
                "data": "gpa"
            },
            {
                "data": "name"
            },
            {
                "data": "roleName"
            },
            {
                "data": null,
                "render": function (data, type, row) {
                    return `<button type="button" class="btn btn-primary" data-toggle="modal" data-target=".bd-example-modal-lg">
                                  Edit
                            </button>
                            <button type="button" class="btn btn-danger" data-toggle="modal" data-target=".bd-example-modal-lg">
                                  Delete
                            </button>
                            `;
                }
            }
        ],

        "aoColumnDefs": [
            { 'bSortable': false, 'aTargets': [10] }
        ]
    });

    setInterval(function () {
        table.ajax.reload();
    }, 30000)
});

function Insert(item) {
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        url: "https://localhost:44361/api/employees/register",
        type: "POST",
        contentType: "application/json",
        data: JSON.stringify(obj)
    }).done((result) => {
           alert('berhasil')
    }).fail((error) => {
           alert('gagal') 
    });
};

function Validation() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.NIK = $("#nik").val();
    obj.FirstName = $("#firstName").val();
    obj.LastName = $("#lastName").val();
    obj.Email = $("#email").val();
    obj.Salary = parseInt($("#salary").val());
    obj.Password = $("#password").val();
    obj.BirthDate = $("#birthDate").val();
    obj.PhoneNumber = $("#phoneNumber").val();
    obj.Degree = $("#degree").val();
    obj.GPA = $("#gpa").val();
    obj.EducationId = $("#educationId").val();
    obj.UniversityId = $("#universityId").val();

    console.log(obj.nik);

    // Regular Expression For Email
    emailReg = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    passReg = /^[0-9a-zA-Z]{8,}$/;

    if (obj.FirstName != '' && obj.LastName != '' && obj.PhoneNumber != ''
        && obj.BirthDate != '' && obj.Salary != ''
        && obj.Email != '' && obj.Password != '' && obj.Degree != ''
        && obj.GPA != '' && obj.UniversityId != '') {
        if (obj.email.match(emailReg)) {
            if (obj.password.match(passReg)) {
                var json = JSON.stringify(obj);
                Insert(json);
                return true;

            } else {
                alert("Invalid Password, Password setidaknya memiliki 8 character")
                return false
            }
        } else {
            alert("Invalid Email Address...!!!");
            return false;
        }
    } else {
        alert("Semua Harus Di Isi!!");
    }
};

function countUniv(name) {
    let count = 0;
    jQuery.ajax({
        url: 'https://localhost:44361/api/employees/showProfile',
        success: function (result) {
            $.each(result, function (key, val) {
                if (val.name == name) {
                    ++count;
                }
            });
        },
        async: false
    });
    return count;
}


let uniA = countUniv("Udinus");
let uniB = countUniv("Amikom");

var optionbar = {
    chart: {
        type: 'bar',
        height: '234px'
    },
    series: [{
        name: 'University ',
        data: [uniA,uniB]
    }],
    xaxis: {
        categories: ["Udinus", "Amikom"]
    }
}
var chart = new ApexCharts(document.querySelector("#barChart"), optionbar);
chart.render();
