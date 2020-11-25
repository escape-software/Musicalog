$(function () { //DOM Ready

});

/*
    Save a new or existing Album.
    The Album form MVC validation is triggered and if there are no validation errors then post form to server
    using Ajax HttpPost.
    If the Album was successfully saved then redirect to Album List, else display errors in the form.
*/
function saveAlbum() {

    if (confirm('Are you sure you wish to save album details?')) {

        // Validate the form using MVC validation.
        $.validator.unobtrusive.parse($('#formAlbum'));
        $('#formAlbum').validate();

        // Check if form is valid.
        if ($('#formAlbum').valid()) {

            // If form is valid and confirmed by user then ajax post details to server.
            $.ajax({
                url: $('#formAlbum').attr('action'),
                type: "POST",
                async: false,
                data: $('#formAlbum').serialize(),
                dataType: "json",
                beforeSend: function () {
                    $('body').css('cursor', 'progress');
                },
                success: function (data, textStatus, jqXHR) {
                    if (data.Success) {
                        alert("Album was successfully saved.");
                        window.location = $('input[id*=hfApplicationRoot]').val();
                    }
                    else {
                        // Failed to save album. Display view with errors.
                        $('#mainBody').html(data.View);
                        alert("Album could not be saved.");

                        // Reset validation.
                        resetFormValidation();
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert("Could not save album details. " + errorThrown);
                },
                complete: function (jqXHR, textStatus) {
                    $('body').css('cursor', 'auto');
                }
            });
        }
    }
    return false;
}

/*
    When the Cancel button is clicked redirect to Album List.
*/
function cancelAlbum() {
    if (confirm('Are you sure you wish to cancel any album changes?')) {
        window.location = $('input[id*=hfAlbumListURL]').val();
    }
}

/*
    Remove any form MVC validation messages.
*/
function resetFormValidation() {
    $('.field-validation-error').each(function () {
        $(this).html("");
    });

    $('.validation-summary-errors').each(function () {
        $(this).html("");
    });
}

