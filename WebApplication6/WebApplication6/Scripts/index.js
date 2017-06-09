function showAlert(title, content) {
    $.alert({
        title: title,
        content: content,
    });
}

function showConfirm(title, content, confirm, cancel) {
    $.confirm({
        title: title,
        draggable: true,
        content: content,
        buttons: {
            confirm: confirm,
            cancel: cancel
        }
    });
}

function deleteImage(img) {
    showConfirm('Delete!', 'Do you want to delete this image',
        function () {
            $.ajax({
                type: "POST",
                url: _deleteAction,
                data: { id: $(img).attr('id') },
                cache: false,
                success: function (data) {
                    showAlert('Deleted!', data);
                    $(img).remove();
                }
            });
        },
        function () {
        }
    );
}

$(document).ready(function () {

    $("#Upload").click(function () {
        var formData = new FormData();
        var arr = [];
        var totalFiles = document.getElementById("FileUpload").files.length;
        for (var i = 0; i < totalFiles; i++) {
            var file = document.getElementById("FileUpload").files[i];
            formData.append("file", file);
        }
        $.ajax({
            type: "POST",
            url: _uploadAction,
            data: formData,
            contentType: false,
            processData: false,
            success: function (response) {
                for (var i = 0; i < response.length; i++) {
                    var imagePath = '<img id=' + response[i] + ' onclick="deleteImage(this)" style="width:400px; height:400px;" src=' + _getImage + '/' + response[i] + ' />';
                    $('#images').append(imagePath);
                }
            },
            error: function (error) {
                showAlert('Error', error);
            }
        });
    });


    // Alert button on click event
    $('#btn_alert').click(() => {
        showAlert('Alert', 'Hello samo');
    });

    // Confirm button on click event
    $('#btn_confirm').click(() => {
        showConfirm('Confirm!', 'Simple confirm!', () => { showAlert('Confirmed', 'confirmed') }, () => { showAlert('Cancelled', 'cancelled') });
    });

    // Dialog button on click event
    $('#btn_dialog').click(() => {
        $.confirm({
            title: 'Prompt!',
            content: '<div id="dialog" class="hidden"><form action="" class="formName"><div class="form-group"><div class="md-form samo"><input type="text" id="name" class="form-control"><label for="name">Your name</label></div><div class="md-form"><input type="text" id="age" class="form-control"><label for="age">Your age</label></div></div></form></div>',
            buttons: {
                formSubmit: {
                    text: 'Submit',
                    btnClass: 'btn-blue',
                    action: function () {
                        var name = this.$content.find('#name').val();
                        var age = this.$content.find('#age').val();
                        if (!name) {
                            $.alert('provide a valid name');
                            return false;
                        }
                        $.alert('Your name is ' + name + '<br />' + 'Your age is ' + age);
                    }
                },
                cancel: function () {
                    //close
                },
            },
            onContentReady: function () {
                // bind to events
                var jc = this;
                this.$content.find('form').on('submit', function (e) {
                    // if the user submits the form by pressing enter in the field.
                    e.preventDefault();
                    jc.$$formSubmit.trigger('click'); // reference the button and click it
                });
            }
        });
    });
});