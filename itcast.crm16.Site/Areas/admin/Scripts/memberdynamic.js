var addFlag = 0;
var editId = 0;//0标识新增，1标识修改
var pageIndex = 1;
var pageSize = 15;
var $modal;
var isFirst = true;

$(function () {
    isFirst = false;

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


    //取消
    $("#Cancel").unbind("click").bind("click", function () {
        editId = 0;
        $modal.modal('close');
    })

    SetPageHtml();

    //分类查询
    $("#memberType").change(function () {
        var typeId = $(this).val();
        GetContentByPage(typeId, 1, 10, "");
    })

})


/***********************
获取数据 typeId=0,标识获取全部类型数据
***********************/
function GetContentByPage(typeId,pageIndex, pageSize, searchMsg) {
    var content = {
        "typeid":typeId,
        "pageIndex": pageIndex,
        "PageSize": pageSize,
        "searchMsg": searchMsg
    };
    $.post("/Admin/member/GetContentByPage", content, function (retContent) {
        if (retContent.status == 0) {
            var totalPage = retContent.datas.TotalPage;
            var dataContent = retContent.datas.Content;

            $("#new-pageCount").text(totalPage);
            //将数据展示到页面
            var showHtml = "";
            for (var i = 0; i < dataContent.length; i++) {
                showHtml += '<tr>';
                showHtml += '<td>' + (i + 1) + '</td>';
                showHtml += '<td>' + dataContent[i].typeName + '</td>';
                showHtml += '<td>' + dataContent[i].Title + '</td>';
                showHtml += '<td class="am-hide-sm-only" ><div _switch="" class="am-switch am-round am-switch-success changelook  newDisplay ' + (dataContent[i].IsComment ? "am-active" : "") + '"><div class="am-switch-handle"><input type="checkbox" class="" ' + (dataContent[i].IsComment == 0 ? "checked" : "") + ' data-id="' + dataContent[i].id + '"></div></div></td>';
                showHtml += '<td class="am-hide-sm-only">' + dataContent[i].CreateTime + '</td>';
                showHtml += '<td><div class="am-btn-toolbar"><div class="am-btn-group am-btn-group-xs">';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary member-edit" data-id="' + dataContent[i].id + '"><span class="am-icon-pencil-square-o"></span> 编辑</a>';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only member-del" data-id="' + dataContent[i].id + '"><span class="am-icon-trash-o"></span> 删除</a>';
                showHtml += '</div></div></td></tr>';
            }
            $("#memberTbody").html(showHtml);
        }
    }, "json")

}

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
    $('#my-confirm-Del').modal({
        relatedTarget: this,
        onConfirm: function (options) {
            $.ajax({
                url: "/admin/member/del",
                type: "post",
                data: content,
                dataType: "json",
                success: function (e) {
                    if (e.status == 0) {
                        aDel.parents("tr:first").remove();
                    }
                    else {
                        bfeMsgBox.error("", e.msg);
                    }
                },
                error: function (er) {
                    bfeMsgBox.error("", er.msg);
                }
            })
        },
        // closeOnConfirm: false,
        onCancel: function () {

        }
    });
})

//开启关闭按钮功能
$(document).on('click', '.changelook', function () {
    var content = {
        "id": $(this).find("input[type=checkbox]").attr("data-id"),
        "comment": ($(this).hasClass('am-active') ? 0 : 1)
    }
    $.ajax({
        url: "/admin/member/ChangeComment",
        data: content,
        type: "post",
        datatype: "json",
        success: function (e) {
            if (e.status == 0) {
                bfeMsgBox.success("", "操作成功");
            }
            else {
                bfeMsgBox.error("", "操作失败");
                setTimeOut(function () { window.location.reload(); }, 1000);
            }
        },
        error: function (er) {
            bfeMsgBox.error("", er);
        }
    })
});


$(document).on("click", ".member-edit", function () {
    editId = $(this).attr("data-id");
    var content = {
        "id": editId
    }
    $.post("/admin/member/GetMemberDyNamicById", content, function (e) {
        if (e.status == 0) {
            $("#doc-ipt-text-1").val(e.datas.Title);
            $("#selectType").find("option[value='" + e.datas.Type + "']").attr("selected", true);
            editor.setContent(e.datas.Content);
            $modal.modal({ closeViaDimmer: 0, width: 800, height: 800 });
        }
    }, "json")
})


$(document).on("click", "#seachNew", function () {
    GetContentByPage(0,1, 10, $("#searchName").val());
})

function SetPageHtml() {
    var pageCount = $("#pageDemo").attr("data-PageCount");
    var pageIndex = $("#pageDemo").attr("data-indexPage");
    var searchContent = $("#searchName").val();
    $('#pageDemo').page({
        pages: pageCount,
        first: "首页", //设置false则不显示，默认为false  
        last: "尾页", //设置false则不显示，默认为false      
        prev: '<', //若不显示，设置false即可，默认为上一页
        next: '>', //若不显示，设置false即可，默认为下一页
        groups: 5, //连续显示分页数
        jump: function (context) {
            if (!isFirst) {
                GetContentByPage(0,context.option.curr, 10, searchContent);
            }
        }//这里就是去异步请求方法
    });
}