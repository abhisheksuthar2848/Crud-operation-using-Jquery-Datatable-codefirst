debugger
var studentDTO = [];

$(document).on('click', '#btnSaveStudent', function () {

    validation()

});

function validation() {
    debugger
    if ($('#txtfirstname').val() == "") {

        alert("Please enter name value");
    }
    else if ($('#txtaddress').val() == "") {

        alert("please enter address");
    }
    else if ($('#txthobbies').val() == "") {
        alert("please enter hobbies");
    }
    else if ($('#txtgender').val() == "") {
        alert("please enter gender");
    }

    else if ($('#txtclass').val() == "") {
        alert("please enter class");
    }
    else {
         debugger
        addStudentData();
    }
}


function addStudentData() {

    debugger
    var name = $('#txtfirstname').val();
    var address = $('#txtaddress').val();
    var hobbies = $('#txthobbies').val();
    var gender = $('#txtgender').val();
    var studentclass = $('#txtclass').val();


    studentDTO.push({

        id: studentDTO.length + 1,
        name: name,
        address: address,
        hobbies: hobbies,
        gender: gender,
        studentclass: studentclass,
    })
    displayStudent();
    $('#txtfirstname').val("");
    $('#txtaddress').val("");
    $('#txthobbies').val("");
    $('#txtgender').val("");
    $('#txtclass').val("");
}

function displayStudent() {

     debugger
    var htmlString = "";
    for (var i = 0; i < studentDTO.length; i++) {
        htmlString += `<tr>
                          <td class="student-name">${studentDTO[i].name}</td>
                          <td class="student-address">${studentDTO[i].address}</td>
                          <td class="student-hobbies">${studentDTO[i].hobbies}</td>
                          <td class="student-gender">${studentDTO[i].gender}</td>
                          <td class="student-class">${studentDTO[i].studentclass}</td>
                          <td>
                                <button class ="btn btn-info btnEdit" data-id="${studentDTO[i].id}">Edit</button>
                                <button class ="btn btn-info btnSave hidden" data-id="${studentDTO[i].id}">Save</button>
                                <button class ="btn btn-danger btnDelete" data-id="${studentDTO[i].id}">Delete</button>
                         </td>            
                   </tr>`;
    }
    $('#tblStudentList tbody').html(htmlString);

}

$(document).on('click', '.btnDelete', function () {
     debugger
    studentDTO = studentDTO.filter(x => x.id != $(this).attr('data-id'));
    displayStudent();
});

$(document).on('click', '.btnEdit', function () {
    debugger
    var currentData = studentDTO.filter(x => x.id == $(this).attr('data-id'))[0];
    if (currentData != undefined) {
        var parentRow = $(this).parents('tr');

        $(parentRow).find('.student-name').html(`<input type="text" id="name" class="col-md-6" value="${currentData.name}" />`);
        $(parentRow).find('.student-address').html(`<input type="text" id="address" class="col-md-6" value="${currentData.address}" />`);
        $(parentRow).find('.student-hobbies').html(`<input type="text" id="hobbies" class="col-md-6" value="${currentData.hobbies}" />`);
        $(parentRow).find('.student-gender').html(`<input type="text" id="gender" class="col-md-6" value="${currentData.gender}" />`);
        $(parentRow).find('.student-class').html(`<input type="text" id="class" class="col-md-6" value="${currentData.studentclass}" />`);


        $(this).addClass('hidden');
        $(this).parent().find('.btnSave').removeClass('hidden');
    }
});


$(document).on('click', '.btnSave', function () {
    debugger

    var currentData = studentDTO.filter(x => x.id == $(this).attr('data-id'))[0];
    var id = $(this).attr('data-id');
    //studentDTO.pop({
    //    id: currentData.id,
    //})

    studentDTO = $.grep(studentDTO, function (data, index) {
        return data.id != id
    });


    studentDTO.push({

        id: currentData.id,
        name: $('#name').val(),
        address: $('#address').val(),
        hobbies: $('#hobbies').val(),
        gender: $('#gender').val(),
        studentclass: $('#class').val(),
    })

    displayStudent();



});