var modal;
isFirst = true;

$(function () {
    modal = $("#doc-modal-1");
    SetPageHtml();
    isFirst = false;
})

//会员详细信息
$(document).on("click", ".detailed-model", function () {
    //获取会员数据
    var content = {
        "id": $(this).attr("data-id")
    }
    $.ajax({
        url: "/admin/member/getmembermsg",
        type: "get",
        datatype: "json",
        data: content,
        success: function (e) {
            if (e.status == 0) {
                $("#doc-loginname").val(e.datas.entity.LoginName);
                $("#doc-realname").val(e.datas.entity.RealName);
                $("#doc-gender").val(e.datas.entity.Gender == true ? "男" : "女");
                $("#doc-address").val(e.datas.entity.Address);
                $("#doc-enterprise").val(e.datas.entity.Enterprise);
                $("#doc-post").val(e.datas.entity.Post);
                $("#doc-contact").val(e.datas.entity.Contact);
                $("#doc-cellphone").val(e.datas.entity.CellPhone);
                $("#doc-email").val(e.datas.entity.Email);
                $("#doc-contactaddress").val(e.datas.entity.ContactAddress);
                $("#doc-postcode").val(e.datas.entity.Postcode);
                $("#doc-isformal").val(e.datas.entity.IsFormal == true ? "正式会员" : "非正式会员");
                $("#doc-ispass").val(e.datas.entity.IsPass == true ? "通过" : "未通过");
                $("#doc-whopass").val(e.datas.entity.WhoPass);
                $("#doc-passtime").val(e.datas.PassTimeStr);
                modal.modal({ closeViaDimmer: 0, width: 700, height: 900 })
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

//通过审核
$(document).on("click", "pass-model", function () {
    //获取会员数据
    var content = {
        "id": $(this).attr("data-id")
    }
    $.ajax({
        url: "/admin/member/PassMemberMsg",
        type: "get",
        datatype: "json",
        data: content,
        success: function (e) {
            if (e.status == 0) {
                 bfeMsgBox.success("", "审核成功");
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

//删除
$(document).on("click", "del-model", function () {
    //获取会员数据
    var content = {
        "id": $(this).attr("data-id")
    }
    $.ajax({
        url: "/admin/member/DelMemberMsg",
        type: "get",
        datatype: "json",
        data: content,
        success: function (e) {
            if (e.status == 0) {
                bfeMsgBox.success("", "删除成功");
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

//分页获取数据
function SetPageHtml() {
    var pageCount = $("#pageDemo").attr("data-PageCount");
    var pageIndex = $("#pageDemo").attr("data-indexPage");
    var searchContent = $("#txtSearchContent").val();
    $('#pageDemo').page({
        pages: pageCount,
        first: "首页", //设置false则不显示，默认为false  
        last: "尾页", //设置false则不显示，默认为false      
        prev: '<', //若不显示，设置false即可，默认为上一页
        next: '>', //若不显示，设置false即可，默认为下一页
        groups: 5, //连续显示分页数
        jump: function (context) {
            if (!isFirst) {
                GetContentByPage(context.option.curr, 10, searchContent);
            }
        }//这里就是去异步请求方法
    });
}

/***********************
获取数据
***********************/
function GetContentByPage(pageIndex, pageSize, searchMsg) {
    var content = {
        "PageIndex": pageIndex,
        "PageSize": pageSize,
        "RealName": searchMsg
    };
    $.post("/Admin/member/GetMemberMsgByPage", content, function (retContent) {
        if (retContent.status == 0) {
            var totalPage = retContent.datas.TotalPage;
            var dataContent = retContent.datas.memberList;

            $("#new-pageCount").text(totalPage);
            //<tr>
            //    <td>1</td>
            //    <td>Jay</td>
            //    <td>张文杰</td>
            //    <td>男</td>
            //    <td>广州市以纯网络科技有限公司</td>
            //    <td>董事长</td>
            //    <td style="color:red">否</td>
            //    <td style="color:red">否</td>
            //    <td>
            //            <div class="am-btn-toolbar">
            //                <div class="am-btn-group am-btn-group-xs">
            //                    <a class="am-btn am-btn-default am-btn-xs am-text-secondary detailed-model" data-id="1"><span class="am-icon-pencil-square-o"></span>详细</a>
            //                    <a class="am-btn am-btn-default am-btn-xs am-text-secondary pass-model" data-id="1"><span class="am-icon-pencil-square-o"></span>审核</a>
            //                    <a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only del-model" data-id="1"><span class="am-icon-trash-o"></span> 删除</a>
            //                    <a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only del-model" data-id="1"><span class="am-icon-trash-o"></span> 转正式</a>
            //                </div>
            //            </div>
            //    </td>
            //</tr>
            //将数据展示到页面
            var showHtml = "";
            for (var i = 0; i < dataContent.length; i++) {
                showHtml += '<tr>';
                showHtml += '<td>' + (i + 1) + '</td>';
                showHtml += '<td>' + dataContent[i].LoginName + '</td>';
                showHtml += '<td>'+dataContent[i].RealName+'</td>';
                showHtml += '<td>'+(dataContent[i].Gender==true?"男":"女")+'</td>';
                showHtml += '<td>'+dataContent[i].Enterprise+'</td>';
                showHtml += '<td>'+dataContent[i].Post+'</td>';
                showHtml += '<td style="color:'+(dataContent[i].IsFormal==true?"green":"red")+'">'+(dataContent[i].IsFormal==true?"是":"否")+'</td>';
                showHtml += '<td style="color:' + (dataContent[i].IsPass == true ? "green" : "red") + '">' + (dataContent[i].IsPass == true ? "是" : "否") + '</td>';
                showHtml += '<td><div class="am-btn-toolbar"><div class="am-btn-group am-btn-group-xs">';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary detailed-model" data-id="'+dataContent[i].Id+'"><span class="am-icon-pencil-square-o"></span> 详细</a>';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary pass-model" '+(dataContent[i].IsPass==true?"style=\"display:none;\"":"")+' data-id="'+dataContent[i].Id+'"><span class="am-icon-pencil-square-o"></span> 审核</a>';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-danger am-hide-sm-only del-model" data-id="'+dataContent[i].Id+'"><span class="am-icon-trash-o"></span> 删除</a>';
                showHtml += '<a class="am-btn am-btn-default am-btn-xs am-text-secondary formal-model" '+(dataContent[i].IsFormal==true?"style=\"display:none;\"":"")+' data-id="'+dataContent[i].Id+'"><span class="am-icon-trash-o"></span> 转正式</a>';
                showHtml += '</div></div></td></tr>';
            }
            $("#showTb").html(showHtml);
        }
    }, "json")

}

//查询
$(document).on("click", "#btnSearch", function () {
    GetContentByPage(1,10,$("#txtSearchContent").val());
})


//审核
$(document).on("click", ".pass-model", function () {
    var id = $(this).attr("data-id");
    $("#tipmessage").text("确定审核通过吗？");
    $('#my-confirm-Del').modal({
        relatedTarget: this,
        onConfirm: function (options) {
            $.ajax({
                url: "/admin/member/PassMemberMsg",
                data: { "id": id },
                type: "post",
                dataType: "json",
                success: function (e) {
                    if (e.status == 0) {
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
        },
        // closeOnConfirm: false,
        onCancel: function () {

        }
    });
})


//删除
$(document).on("click", ".del-model", function () {
    var id = $(this).attr("data-id");
    $("#tipmessage").text("确定删除该会员信息吗？");
    $('#my-confirm-Del').modal({
        relatedTarget: this,
        onConfirm: function (options) {
            $.ajax({
                url: "/admin/member/DelMemberMsg",
                data: { "id": id },
                type: "post",
                dataType: "json",
                success: function (e) {
                    if (e.status == 0) {
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
        },
        // closeOnConfirm: false,
        onCancel: function () {

        }
    });
})

//转正式会员
$(document).on("click", ".formal-model", function () {
    var id = $(this).attr("data-id");
    $("#tipmessage").text("确定转为正式会员吗？");
    $('#my-confirm-Del').modal({
        relatedTarget: this,
        onConfirm: function (options) {
            $.ajax({
                url: "/admin/member/FormalMemberMsg",
                data: { "id": id },
                type: "post",
                dataType: "json",
                success: function (e) {
                    if (e.status == 0) {
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
        },
        // closeOnConfirm: false,
        onCancel: function () {

        }
    });
})
