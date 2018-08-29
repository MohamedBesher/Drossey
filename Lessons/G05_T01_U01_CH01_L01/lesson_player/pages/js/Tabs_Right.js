       var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
var sound_click ='../sound/click';

var narration;
$(document).ready(function() {  
    // this key code to link with player and load json 
     /*   loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Tabs_Right.js', function (a, b) {}).onload = function () {*/
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            start();
        };
   // }
//end key code 
});

function loadScript(path, callback) {
    var done = false;
    var scr = document.createElement('script');
    scr.onload = handleLoad;
    scr.onreadystatechange = handleReadyStateChange;
    scr.onerror = handleError;
    scr.src = path;
    document.body.appendChild(scr);

    function handleLoad() {
        if (!done) {
            done = true;
            callback(path, "ok");
        }
    }

    function handleReadyStateChange() {
        var state;
        if (!done) {
            state = scr.readyState;
            if (state === "complete") {
                handleLoad();
            }
        }
    }

    function handleError() {
        if (!done) {
            done = true;
            callback(path, "error");
        }
    }
    return scr;
}
function start() {
//    $("#repeat_btn").bind("click",function(e){
//        history.go(0);
//    });
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    }); 
    
    //page header
    if(mydata.staticData.pageHeader != undefined){
        $('#lesson_title').html(mydata.staticData.pageHeader);
    }else{
        $('.alhaytham_head').css('display','none');
    }
    
    //prompt
    if(mydata.staticData.prompt != undefined){
        $('.notification_msg p').html(mydata.staticData.prompt);
    }else{
        $('.notification_msg').css('display','none');
    }
    
    //page sub-title
    if(mydata.staticData.inside_title !='' && mydata.staticData.inside_title !=undefined){
        $('.ttitle').prepend('<p class="page_desc text-justify">'+mydata.staticData.inside_title+'</p>')
    }
    //page title
    if(mydata.staticData.title !='' && mydata.staticData.title != undefined){
        $('.ttitle').prepend('<h2 class="tt title_font page_title">'+mydata.staticData.title+'</h2>')
    }
    
    //narration on page start
    if(mydata.staticData.narration != undefined && mydata.staticData.narration !=''){
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=canPlayMP3?mydata.staticData.narration+'.ogg':mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();
        
        $('#narration').on('ended', function() {
            play_firstTab_audio();
        });
    }else{
        play_firstTab_audio();
    }
    loadTabs();    
}

//load tabs
function loadTabs(){
    for(var x=0;x<mydata.dynamicData.length;x++){
        if(mydata.dynamicData[x].title_icon != '' && mydata.dynamicData[x].title_icon != undefined){
            $('.btn_list').append('<a href="#"><img class="ticon" src="'+mydata.dynamicData[x].title_icon+'"/> '+mydata.dynamicData[x].title+'</a>');
        }else{
            $('.btn_list').append('<a href="#">'+mydata.dynamicData[x].title+'</a>');
        }
                
    }
    $('.btn_list a').first().addClass('active');
    load_tab_content(0);
    
    //on click on tab content
    $('.btn_list a').click(function(){
        //play sound effect
        var snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=canPlayMP3?sound_click+'.ogg':sound_click+'.mp3';
        snd.load();
        snd.play();
        
        if($('.btn_list a').hasClass('active')){
            $('.btn_list a').removeClass('active');
        }
        $(this).addClass('active');
        
        load_tab_content($(this).index());        
        
        //play tab narration
        if(mydata.dynamicData[$(this).index()].sub_narration != undefined && mydata.dynamicData[$(this).index()].sub_narration != ''){
            narration=document.getElementById('narration');
            canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
            narration.src=canPlayMP3?mydata.dynamicData[$(this).index()].sub_narration+'.ogg':mydata.dynamicData[$(this).index()].sub_narration+'.mp3';
            narration.load();
            narration.play();
            $('#narration').on('ended', function() {
                    narration.pause();
            });
        }else{
            narration.pause();
        }
    });
}

function load_tab_content(jsonPosition){
    if(mydata.dynamicData[jsonPosition].type== 'text'){
        $('.sub_cntent').html('<p>'+mydata.dynamicData[jsonPosition].paragraph+'</p>');
    
    }else if(mydata.dynamicData[jsonPosition].type== 'image'){
        $('.sub_cntent').html('<img src="'+mydata.dynamicData[jsonPosition].mediaSource+'"/>');
    
    }else if(mydata.dynamicData[jsonPosition].type== 'video'){
        $('.sub_cntent').html('<video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.mp4" type="video/mp4"/></video>');
    
    }else if(mydata.dynamicData[jsonPosition].type== 'slider'){
        $('.sub_cntent').html('<div class="media_cont"></div>');
        
        loadSlider(jsonPosition)
    
    }else if(mydata.dynamicData[jsonPosition].type== 'textWithImage'){
        $('.sub_cntent').html('<div class="col-xs-6 col-md-6 col-sm-6"><article><p>'+mydata.dynamicData[jsonPosition].paragraph+'</p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><img src="'+mydata.dynamicData[jsonPosition].mediaSource+'"/></div>');
    
    }else if(mydata.dynamicData[jsonPosition].type== 'textWithVideo'){
        $('.sub_cntent').html('<div class="col-xs-6 col-md-6 col-sm-6"><article><p>'+mydata.dynamicData[jsonPosition].paragraph+'</p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.mp4" type="video/mp4"/></video></div>');
    
    }else if(mydata.dynamicData[jsonPosition].type== 'TextWithSlider'){
        $('.sub_cntent').html('<div class="col-xs-6 col-md-6 col-sm-6"><article><p>'+mydata.dynamicData[jsonPosition].paragraph+'</p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><div class="media_cont"></div></div>');
        
        loadSlider(jsonPosition);
    }    
}

//to load slider on screen
function loadSlider(newn){
    $('.media_cont').append('<div id="carousel" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a class="left carousel-control" href="#carousel" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>');
            
    for(var nn=0;nn<mydata.dynamicData[newn].mediaSource.length;nn++){
        $('#carousel ol').append('<li data-target="#carousel" data-slide-to="'+nn+'"></li>');
        $('#carousel .carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[newn].mediaSource[nn]+'"></div>');
    }
    
    $('#carousel li:nth-child(1), #carousel .carousel-inner div:nth-child(1)').addClass('active');    
}

function play_firstTab_audio(){
     if(mydata.dynamicData[0].sub_narration != undefined && mydata.dynamicData[0].sub_narration != ''){
    narration=document.getElementById('narration');
    canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
    narration.src=canPlayMP3?mydata.dynamicData[0].sub_narration+'.ogg':mydata.dynamicData[0].sub_narration+'.mp3';
    narration.load();
    narration.play();
    $('#narration').on('ended', function() {
            narration.pause();
    });
     }
    else{
           //narration.pause();
    }
}















