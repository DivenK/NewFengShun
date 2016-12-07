var addFlag = 0;
var editId = 0;//0标识新增，1标识修改
var pageIndex = 1;
var pageSize = 15;
var $modal;
var isFirst = true;

$(function () {
    //控制新增的弹层
    $modal = $('#doc-modal-1');//弹窗div  


    //发布
    $("#addmember").click(function () {
        editId = 0;
        editor.setContent("");
        $("doc-ipt-text-1").val("");
        $modal.modal({ closeViaDimmer: 0, width: 800, height: 800 });//弹起
    })

    //保存新增或者修改
    $("#formSubmit").bind("click", function () {
        var title = $("#doc-ipt-text-1").val();
        if (title == "") {
            $("#doc-ipt-text-1").focus();
            return;
        }
        var postContent = {
            "id": editId,
            "title": title,
            "type": $("#selectType").val(),
            "content": editor.getContent()
        }
        $.ajax({
            url: "/admin/member/Change",
            data: postContent,
            type: "post",
            dataType: "json",
            success: function (e) {
                if (e.status == 0) {
                    bfeMsgBox.success("", e.msg);
                    window.location.reload();
                }
                else {
                    bfeMsgBox.error("", e.msg);
                }
            },
            error: function (er) {
                bfeMsgBox.error("", er);
            }
        })
    })
})


$("#Cancel").unbind("click").bind("click", function () {
    editId = 0;
    $modal.modal('close');
})

//删除
$(document).on("click", ".member-del", function () {
    var content = {
        "id": $(this).attr("data-id")
    }
    var aDel = $(this);
    $.ajax({
        url: "/admin/member/del",
        type: "post",
        data: content,
        dataType: "json",
        success: function (e) {
            if (e.status == 0) {
                aDel.parents("tr:first").remove();
                bfeMsgBox.success("", e.msg);
            }
            else {
                bfeMsgBox.error("", e.msg);
            }
        },
        error: function (er) {
            bfeMsgBox.error("", er.msg);
        }
    })
})

