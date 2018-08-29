$(window).on("popstate", function (e) {
    location.reload();
    console.log("popstate");
});
$(function () {
   
    console.log($("#CountryId").val());
    console.log($("#GradeId").val());
    $("select").each(function () {
        if ($(this).find("option").length <= 1) {
            $(this).attr("disabled", "disabled");
        }
    });
    $("select").change(function () {

        var value = 0;
        if ($(this).val() !== "") {
            value = $(this).val();
        }
        var id = $(this).attr("id");
        if (id === "Time_zone" || id === "Presenter_email" || id ==="Language_culture_name")
            return;

        $.ajax({
            type: "POST",
            url: "/Library/LoadDrp",
            data: JSON.stringify({ "name": id, "id": value }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var dropDownId;
                var list;
                switch (id) {
                    case "CountryId":
                        list = response;
                        DisableDropDown("#TermId");
                        DisableDropDown("#GradeId");
                        DisableDropDown("#SubjectId");
                        DisableDropDown("#BookId");
                        DisableDropDown("#LessonId");
                        PopulateDropDown("#GradeId", list);
                        break;
                    case "GradeId":
                        list = response;
                        DisableDropDown("#TermId");
                        DisableDropDown("#SubjectId");
                        DisableDropDown("#BookId");
                        DisableDropDown("#LessonId");
                        PopulateDropDown("#TermId", list);
                        break;

                    case "TermId":
                        list = response;
                        DisableDropDown("#SubjectId");
                        DisableDropDown("#BookId");
                        DisableDropDown("#LessonId");
                        PopulateDropDown("#Subject", list);
                        PopulateDropDown("#SubjectId", list);
                        break;


                }

            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
});

function DisableDropDown(dropDownId, type) {

    console.log(dropDownId);
    var txt = "----اختر---";
    switch (dropDownId) {
        case "#GradeId":
            txt = "---  اختر الصف ---";
            break;
        case "#SubjectId":
            txt = "---  اختر المادة ---";
            break;
        case "#Subject":
            txt = "---  اختر المادة ---";
            break;
        case "#BookId":
            txt = "---  اختر الوحدة ---";
            break;

        case "#ModuleId":
            txt = "---  اختر الوحدة ---";
            break;
        case "#LessonId":
            txt = "---  اختر الدرس ---";
            break;
        case "#TermId":
            txt = "---  اختر الترم ---";
            break;


    }

    $(dropDownId).attr("disabled", "disabled");
    $(dropDownId).empty().append('<option selected="selected" value="0">' + txt + '</option>');
}



function PopulateDropDown(dropDownId, list) {
    if (list != null && list.length > 0) {
        $(dropDownId).removeAttr("disabled");
        $.each(list, function () {
            $(dropDownId).append($("<option></option>").val(this['id']).html(this['name']));
        });
    }
}


