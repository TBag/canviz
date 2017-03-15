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
        $(".content-claim-history tr.history-tr.claim").click(function (e) {
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
            var result = 'Claim ' + operationStr;
            if(isApprove)
                result += ' and sent to external vendor';
            result += '<span>Undo</span>';
            return result;
        })();
        $.ajax({
            dataType: 'json',
            url: relativeUrl,
            data: postData,
            contentType: 'application/json',
            type: "post",
            success: function (result) {
                console.log(result);
                $(".content-claim-status .progtrckr").hide();
                var submitResult = $(".content-core .submit-result");
                submitResult.html(submitResultMessage);
                submitResult.removeClass('hide');
                $('.content-core .content-claim .status').html(operationStr);
                if(!isApprove)
                    submitResult.addClass("decline");
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
        $(".operate-button.approve").click(function () {
            console.log('approve');
            claimHelper.approveClaim(true);
        });
        $(".operate-button.decline").click(function () {
            console.log('decline');
            claimHelper.approveClaim(false);
        });
    },
    init: function () {
        claimHelper.initClaimDetail();
        claimHelper.initSignout();
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
                currentTr.addClass("highRisk");
                var riskTd = $(currentTr.children("td")[3]);
                riskTd.html("High");
                riskTd.addClass("risk-td");
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
                self.$contextMenu().remove();
            }
        });
    },
    init: function () {
        contextMenuHelper.initContextMenu();
    }
};

(function () {
    console.log('enter claim.');
    navHelper.init();
    claimHelper.init();
    contextMenuHelper.init();
})();