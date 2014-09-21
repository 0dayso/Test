var chekedarr = new Array(), chkseatbook = new Array(), objtd, te, seatrequired, objtd, i = 0, _seatCoun = 0;
function selectSeats(v, s, c) {
    seatrequired = s;
    var els = getDoc(v).getElementsByTagName('img');
    for (i = 0; i < els.length; ++i) {
        var u = els[i];
        te = u.parentNode.id.split('_')[1];
        if (te == c) {
            u.onmouseover = set_cursor;
            u.onclick = showA;
        }
    }
}
function showA() {
    if (_seatCoun < seatrequired) {
        chkseatbook = gS(this);
    }
    if (_seatCoun >= seatrequired)
        uChkS();
    else
        chkS();
}
function chkS() {
    for (i = 0; i < chkseatbook.length; i++) {
        if (!chkseatbook[i])
            break;
        else {
            getDoc(chkseatbook[i]).firstChild.src = "../../images/G_chair.gif";
            chekedarr.push(chkseatbook[i]);
            _seatCoun++;
        }
    }
}
function uChkS() {
    for (i = 0; i < chekedarr.length; i++) {
        getDoc(chekedarr[i]).firstChild.src = "../../images/W_chair.gif";
    }
    chekedarr = new Array();
    _seatCoun = 0;
}
function gS(e) {
    var chkidarr = new Array();
    var temp1 = e.parentNode.parentNode.getElementsByTagName("td");
    objtd = e.parentNode.id;
    for (i = 0; i < temp1.length; ++i) {
        var tempchk = temp1[i].getElementsByTagName('img');
        if (tempchk.length > 0) {
            for (var ii = 0; ii < tempchk.length; ii++) {
                chkidarr[i] = temp1[i].id;
            }
        }
    }
    for (i = 0; i < chkidarr.length; i++) {
        if (chkidarr[i] == objtd) {
            break;
        }
    }
    return chkidarr.slice(i, i + (seatrequired - _seatCoun));
}
function set_cursor() {
    this.style.cursor = "Pointer";
}
function lTS(e) {
    if (_seatCoun == seatrequired && _seatCoun > 0) {
        var temarr = new Array();
        for (i = 0; i < chekedarr.length; i++) {
            temarr[i] = chekedarr[i].split("Seat_")[1];
        }
        chekedarr = new Array();
        for (i = 0; i < temarr.length; i++) {
            chekedarr[i] = temarr[i].split('_')[1] + '_' + temarr[i].split('_')[2];
        }
        getDoc(e).value = _seatCoun + '|' + temarr[0].split('_')[0] + '|' + chekedarr.join('|');
        return true;
    }
    else if (_seatCoun > seatrequired) {
        alert("You cannot select more then " + seatrequired + " seats, please select exectly " + seatrequired + " seats.");
        return false;
    }
    else if (_seatCoun < seatrequired) {
        alert("Please select " + seatrequired + " seats.");
        return false;
    }
}
function gADef(v, s, c) {
    seatrequired = s;
    var els = getDoc(v).getElementsByTagName('img');
    var P = 0;
    for (i = 0; i < els.length; ++i) {
        var u = els[i];
        te = u.parentNode.id.split('_')[1];
        if (te == c) {
            chekedarr[P] = u.parentNode.id;
            P++;
            if (P == s) {
                break;
            }
        }
    }
    for (i = 0; i < chekedarr.length; i++) {
        getDoc(chekedarr[i]).firstChild.src = "../../images/G_chair.gif";
        _seatCoun++;
    }
}
function getDoc(d) {
    return document.getElementById(d);
}
function printPreviewDiv(s) {
    var WP = window.open('', '', 'left=2,top=5,width=800,height=600,toolbar=0,scrollbars=0,status=0,fullscreen=1');
    WP.document.write(getDoc(s).innerHTML);
    WP.document.close();
    WP.focus();
    WP.print();
    WP.close();
}