var debug = true,
    targetMoney = '428000', // 目标金额
    offlineUrl = "offline/南京市国有建设用地使用权公开出让网上交易系统.htm",
    autoPost = false, // 是否自动提交，默认false
    timer; // 定时器

function showMsg(msg) {
    window.alert(msg);
}

function updateFrameLoad() {
    //var nowMoney = $("#ctl00_ContentPlaceHolder1_lblNewPrice").html().split('：')[1].replace('万元', '');

    //var valiatePageDocument = $("iframe", $('#aspnetForm')).eq(1).prop('contentWindow').document,
    //    MoneyTextElement = $("#txtUserPrice", valiatePageDocument), // 报价金额
    //    MoneyPlusElement = $("#BtnUp", valiatePageDocument), // 加号按钮
    //    confirmButton = $("#ctl00_ContentPlaceHolder1_imgBtn"),
    //    MoneySubmitElement = $("input[value='确认报价']", valiatePageDocument); // 提交按钮

    //if (parseInt(targetMoney) === parseInt(nowMoney)) {
    //    stop();
    //    if (autoPost) {
    //        //confirmButton.trigger('click');
    //        javascript: __doPostBack('ctl00_ContentPlaceHolder1_btnUp', '');
    //        showMsg("自动提交：当前值已达到目标值" + nowMoney + "万元，已提交");
    //    } else {
    //        MoneySubmitElement.trigger('click');
    //        showMsg("手动提交：当前值已达到目标值" + nowMoney + "万元，已提交");
    //    }

    //}

    //if (parseInt(targetMoney) < parseInt(nowMoney)) {
    //    stop();
    //    showMsg("当前值超过目标值，退出监控");
    //}

    //if (parseInt(targetMoney) > parseInt(nowMoney)) {
    //    console.log('当前价格:' + nowMoney);
    //    MoneyPlusElement.trigger('click');

    //}
}

function iframeonload() {
    var valiatePageDocument = $("iframe", $('#aspnetForm')).eq(1).prop('contentWindow').document,
        MoneyTextElement = $("#txtUserPrice", valiatePageDocument), // 报价金额
        MoneyPlusElement = $("#BtnUp", valiatePageDocument), // 加号按钮
        confirmButton = $("#ctl00_ContentPlaceHolder1_imgBtn"),
        MoneySubmitElement = $("input[value='确认报价']", valiatePageDocument); // 提交按钮
    var temp = $("#ctl00_ContentPlaceHolder1_lblNewPrice").html().split('：');
    if (temp.length >= 2) {
        var maxMoney = $("#ctl00_ContentPlaceHolder1_lblNewPrice").html().split('：')[1].replace('万元', '');
        if (parseInt(maxMoney) > (parseInt(MoneyTextElement.val()) + 1000)) {
            return;
        }
    }
   

    if (parseInt(targetMoney) === parseInt(MoneyTextElement.val())) {
        stop();
        if (autoPost) {
            //confirmButton.trigger('click');
            javascript: __doPostBack('ctl00_ContentPlaceHolder1_btnUp', '');
            showMsg("自动提交：当前值已达到目标值" + targetMoney + "万元，已提交");
        } else {
            MoneySubmitElement.trigger('click');
            showMsg("手动提交：当前值已达到目标值" + targetMoney + "万元，已提交");
        }
       
    }

    if (parseInt(targetMoney) < parseInt(MoneyTextElement.val())) {
        stop();
        showMsg("当前值超过目标值，退出监控");
    }

    if (parseInt(targetMoney) > parseInt(MoneyTextElement.val())) {
        console.log('当前价格:' + MoneyTextElement.val())
        MoneyPlusElement.trigger('click');

    }
}



function start() {

    $("iframe", $('#aspnetForm')).eq(1).load(function() {
        iframeonload();
    });

    iframeonload();

    //$("iframe", $('#aspnetForm')).eq(0).load(function() {
    //    updateFrameLoad();
    //});
    //updateFrameLoad();
}

function stop() {
    console.log("注销事件");
    timer && window.clearInterval(timer);
    $("iframe", $('#aspnetForm')).eq(1).unbind("load");
}

// 使用方法
// 1. 设置金额 比如 targetMoney = '389000'
// 2. 设置是否自动提交 autoPost = false false 为不自动提交，true 为自动提交
// 3. 启动竞标 start()
// 4. 停止竞标 stop()