;
var navHelper = {
    navTo: function (view) {
        location.href = location.origin + "/Claim/" + view;
    },
    bindLeftMenu: function () {
        /*menu nav*/
        $("ul.menu-nav > li").click(function () {
            if ($(this).hasClass("claims")) {
                navHelper.navTo("index");
            } else if ($(this).hasClass("builders")) {
                navHelper.navTo("builders");
            }
        });
    },
    bindClaimList: function () {
        $(".common-list.claim-list .content-claim-history tr.history-tr").click(function (e) {
            navHelper.navTo("Detail");
        });
    },
    init: function () {
        navHelper.bindLeftMenu();
        navHelper.bindClaimList();
    }
};

var claimHelper = {
    approveClaim:function (isApprove) {
        var postData = JSON.stringify({
            id: 0,
            approved:isApprove
        });
        var relativeUrl = (function () {
            var url = location.protocol + '//' + location.host + '/Claim/Approve';
            return url;
        })();
        var operationStr = isApprove ? "approved" : "declined";
        var submitResultMessage = (function () {
            var result = '';
            if (userHelper.isManager()) {
                result = 'Claim ' + operationStr;
                if (isApprove)
                    result += ' and sent to external vendor';
            } else {
                result = 'Claim submitted for final approval';
            }
            result += '<span>Undo</span>';
            result += '<div class="mdl2"><span class="more"></span></div>';
            return result;
        })();
        var updateStatusBar = function ($bar) {
            $bar.find(".progtrckr-doing").removeClass("progtrckr-doing").addClass('progtrckr-done');
            $bar.find(".progtrckr-todo").first().removeClass("progtrckr-todo").addClass('progtrckr-doing');
        };
        $.ajax({
            dataType: 'json',
            url: relativeUrl,
            data: postData,
            contentType: 'application/json',
            type: "post",
            success: function (result) {
                console.log(result);
                var submitResult = $(".content-core .submit-result");
                submitResult.html(submitResultMessage);
                submitResult.removeClass('hide');
                $('.content-core .content-claim .status').html(operationStr);
                if (!isApprove)
                    submitResult.addClass("decline");
                if (userHelper.isManager()) {
                    updateStatusBar($(".content-claim-status .progtrckr.manager"));
                } else {
                    updateStatusBar($(".content-claim-status .progtrckr.adjuster"));
                }
                $(".content-alert").addClass("approved");
            }
        });
    },
    initSignout:function(){
        $(".nav-logout .sign-out").click(function () {
            console.log('logout');
            location.href = location.protocol + '//' + location.host + '/Account/SignOut';
        });
    },
    initClaimDetail: function () {
        $(".operate-btn.approve").click(function () {
            console.log('approve');
            claimHelper.approveClaim(true);
        });
        $(".operate-btn.decline").click(function () {
            console.log('decline');
            claimHelper.approveClaim(false);
        });
    },
    initSyncClick:function(){
        $(".navbar .syncBtn").click(function () {
            $(".content-vendor").removeClass("hidden");
        });
    },
    init: function () {
        claimHelper.initClaimDetail();
        claimHelper.initSignout();
        claimHelper.initSyncClick();
    }
};

var contextMenuHelper = {
    $contextMenu: function () {
        return $("div.context-menu");
    },
    contextMenuBind: function () {
        var self = contextMenuHelper;
        self.$contextMenu().find("div.item").click(function () {
            if ($(this).hasClass("approve")) {
                claimHelper.approveClaim(true);
            } else if ($(this).hasClass("decline")) {
                claimHelper.approveClaim(false);
            } else if ($(this).hasClass("highRisk")) {
                var currentTr = $(this).parentsUntil(".history-tr").parent();
                //currentTr.addClass("highRisk");
                var riskTd = $(currentTr.children("td")[4]);
                //riskTd.html("High");
                var span = riskTd.find("span");
                span.text("High")
                riskTd.addClass("risk-td").addClass("selected");
                riskTd.next().addClass("selected");
                self.$contextMenu().remove();
            }
            return false;
        });
    },
    initContextMenu: function () {
        var self = contextMenuHelper;
        $(".context-menu-fire.claim").click(function () {
            if (self.$contextMenu().length == 1) {
                self.$contextMenu().remove();
            }
            var contextMenuHtml = '<div class="context-menu"><div class="item approve">Approve</div><div class="item decline">Decline</div><div class="item split-line"></div><div class="item">Contact policy holder</div</div>';
            $(this).append(contextMenuHtml);
            self.contextMenuBind();
            return false;
        });
        $(".context-menu-fire.builder").click(function () {
            if (self.$contextMenu().length == 1) {
                self.$contextMenu().remove();
            }
            var contextMenuHtml = '<div class="context-menu"><div class="item approve">Edit</div><div class="item decline">Remove</div><div class="item split-line"></div><div class="item highRisk">Mark as high risk</div</div>';
            $(this).append(contextMenuHtml);
            self.contextMenuBind();
            return false;
        });
        // hide context menu when click anywhere
        $(document).mouseup(function (e) {
            var _con = self.$contextMenu();
            if (!_con.is(e.target) && _con.has(e.target).length === 0) {
                var lastTd = self.$contextMenu().parent().parent();
                var riskTd = lastTd.prev();
                lastTd.removeClass('selected');
                riskTd.removeClass('selected');
                self.$contextMenu().remove();

            }
        });
    },
    init: function () {
        contextMenuHelper.initContextMenu();
    }
};

var userHelper = {
    currentRole: '',
    roles: {
        customer: "customer",
        adjuster: "adjuster",
        manager: "manager"
    },
    isAdjuster:function(){
        return userHelper.currentRole == userHelper.roles.adjuster;
    },
    isManager: function () {
        return userHelper.currentRole == userHelper.roles.manager;
    },
    initRole: function () {
        var role = $("#ClaimUserRole").val();
        if(role)
            userHelper.currentRole = role.toLowerCase();
    },
    changeView: function () {
        if (userHelper.currentRole == userHelper.roles.adjuster) {            
            $(".progtrckr.adjuster").show();
            $(".content-alert.adjuster").show();
            $(".content-alert .content-alert-title").addClass("border-bottom");
        } else if (userHelper.currentRole == userHelper.roles.manager) {
            $(".progtrckr.manager").show();
            $(".content-alert.manager").show();
            $(".content-alert .content-alert-title").addClass("manager");
            $(".power-bi-report").removeClass("hidden");
        }
    },
    init: function () {
        userHelper.initRole();
        userHelper.changeView();
    }
};

var loginHelper = {
    initSubmitClick:function(){
        $("div.login .btns .submit").click(function () {
            navHelper.navTo("index");
        });
    },
    initCancelClick:function(){
        $("div.login .btns .cancel").click(function () {
            $(".username >input").val('');
            $(".password >input").val('');
        });
    },
    init: function () {
        loginHelper.initCancelClick();
        loginHelper.initSubmitClick();
    }
};

var viewHelper = {
    init: function () {
        viewHelper.initResize();
    },
    initResize: function () {
        var resizeHtml = function () {
            var browserWidth = document.documentElement.clientWidth;
            if (browserWidth > 1366) {
                $(".navbar .container").css("max-width", browserWidth);
                $(".content-main .content-operation-area").css("max-width", browserWidth);
                $(".content-main .content-claim-status").css("max-width", browserWidth);
            }
            
        };
        window.onresize = resizeHtml;
        resizeHtml();
    }
};

(function () {
    console.log('enter claim.');
    navHelper.init();
    claimHelper.init();
    contextMenuHelper.init();
    userHelper.init();
    loginHelper.init();
    viewHelper.init();
})();