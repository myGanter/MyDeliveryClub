var HostApp = new Vue({
    el: '#App',
    data: {
        currentView: 'tab2',
        msg: {},
        onDisplay: false,
        logDisplay: true,
        controller: ''
    },
    created() {
        this.controller = $('#Controller').text();
        this.currentView = $('#Method').text();
        $('#MsgWindow').css('display', 'block');
        
        this.setLocation();
    },
    methods: {
        swichView: function (view) {
            this.currentView = view;
            this.setLocation();
        },
        showMsg: function (mes) {
            this.msg = mes;
            this.onDisplay = true;
            console.log(this.msg);
        },
        setLocation: function (curLoc){
            history.pushState(null, null, '/' + this.controller + '/' + this.currentView);            
        }
    }
});