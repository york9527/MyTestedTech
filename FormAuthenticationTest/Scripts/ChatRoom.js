var currentRoom = "";

$(function () {
    
    var chatHub = $.connection.myHub;
    //注册客户端函数，已供服务器端Hub调用
    regesterClientMethod(chatHub);
    
    

    // Start the connection to get events
    $.connection.hub.start().done(function () {
        //连接成功后，绑定客户端UI界面事件，例如click响应函数等
        regesterEvent(chatHub);
        chatHub.state.ExtraInfo = "初始化额外消息";
    });

    //其他事情
    changeCurrentRoom('默认聊天室1');
});

//输出消息到聊天窗口
function addMessage(room, msg, sysName) {
    if (room != currentRoom) return;
    var uName = sysName || $('#userName').val();
    var contentToShou = '<p><b>'
        + uName
        + ' '
        + room
        + ' '
        + new Date().toLocaleString()
        + ':</b></p><div>&nbsp;&nbsp;'
        + msg
        + '</div>';
    $('#MsgContent').append(contentToShou);

    var hid = document.getElementById('msg_end');
    hid.scrollIntoView(false);
}

function changeCurrentRoom(room) {
    currentRoom = room;
    $('#room').text(room);
}


//注册事件

function regesterEvent(chatHub) {
    $('#btSentMsg').on('click', function() {
        var msgObj = $('#Message');
        var msg = msgObj.val();
        msgObj.val("");

        //调用服务器端方法
        //chatHub.server.sendMessageToAll(user, msg);
        chatHub.server.send(currentRoom, msg);
    });

    $('#btClearMsg').on('click', function() {
        $('#Message').val('');
    });

    $('#btCreateRoom').on('click', function() {
        var r = $('#roomName');
        chatHub.server.createChatRoom(r.val());
        changeCurrentRoom(r.val());
        r.val("");
    });

    $('div.RoomWin').on('dblclick','div', function() {
        var room = $(this).text();
        chatHub.server.join(room);
        changeCurrentRoom(room);
    });
    
}

//注册客户端方法，由服务器端调用

function regesterClientMethod(chatHub) {
    chatHub.client.messageReceived = function(userName, message) {
        addMessage(userName, message);
    };

    chatHub.client.addChatRoom = function(room) {
        $('.RoomWin').append('<div>' + room + '</div>');
        addMessage(room, '欢迎来到聊天室：' + room, '系统');
    };

    chatHub.client.addMessage = function (room, msg) {
        var info = "客户端接收的额外信息"+chatHub.state.ExtraInfo;
        addMessage(room, msg+info);
    };
}