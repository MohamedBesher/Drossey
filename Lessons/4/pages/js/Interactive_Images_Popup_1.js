var sound_click ='sounds/click';

var flag=0;
var narration;
var snd;
$(document).ready(function() {
    
    // this key code to link with player and load json 
//        loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
//        loadScript('../Player/lessons/'+lessonID+'/media/Interactive_Images_Popup_1.js', function (a, b) {}).onload = function () {
//            start();
//            
//        };
//    }
    
     var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            start();
            
        };
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
        narration.src=mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();
    }
    
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    });
//    $("#repeat_btn").bind("click",function(e){
//      history.go(0);
//   });
    
    loadLinks();
    
    //on click on image or it's text to view moodle
    $('.image_title a, .image_title img').click(function(){         
        load_Moodle_Data($(this).parent().attr('id'));        
        
        //play sound effect
        snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=sound_click+'.mp3';
        snd.load();
        snd.play();
        
        //play tab narration
        if(mydata.dynamicData[$(this).parent().index()].sub_narration != undefined && mydata.dynamicData[$(this).parent().index()].sub_narration != ''){
            narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=mydata.dynamicData[$(this).parent().attr('id')].sub_narration+'.mp3';
        narration.load();
        narration.play();
        }else{
            narration.pause();
        }    
    });
        
    //event when hide moodle
    $('#text_modal').bind('hidden.bs.modal', function () {
        narration.pause();
        $('.modal_text').empty();
    });    
}

function loadLinks(){
    if(mydata.dynamicData.length==1){
        loadOne();
    }else if(mydata.dynamicData.length==2){
        loadTwo();
    }else if(mydata.dynamicData.length%3==0 || mydata.dynamicData.length==5){
        loadThrees();
    }else if(mydata.dynamicData.length%4==0 || mydata.dynamicData.length%4==2 || mydata.dynamicData.length%4==3){
        loadFours();
    }else{
        loadThrees();
    }
}

function loadOne(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        $('.contents .row').append('<div class="col-xs-12"><div id="'+x1+'" class="image_title wow bounceIn"><img data-toggle="modal" data-target="#text_modal" src="'+mydata.dynamicData[x1].image+'"/><a href="javascript:void(0)" data-toggle="modal" data-target="#text_modal">'+mydata.dynamicData[x1].sample+'</a></div></div>');
    }    
}

function loadTwo(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        $('.contents .row').append('<div class="col-xs-6 col-md-6"><div id="'+x1+'" class="image_title wow bounceIn"><img data-toggle="modal" data-target="#text_modal" src="'+mydata.dynamicData[x1].image+'"/><a href="javascript:void(0)" data-toggle="modal" data-target="#text_modal">'+mydata.dynamicData[x1].sample+'</a></div></div>');
    }
}

function loadThrees(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        flag++;
        if (flag==4){
            flag=1;
            $('.contents .row').append('<div class="clearfix"></div>');
        }
        $('.contents .row').append('<div class="col-xs-4 col-md-4 col-md-4"><div id="'+x1+'" class="image_title wow bounceIn"><img data-toggle="modal" data-target="#text_modal" src="'+mydata.dynamicData[x1].image+'"/><a href="javascript:void(0)" data-toggle="modal" data-target="#text_modal">'+mydata.dynamicData[x1].sample+'</a></div></div>');
    }
}

function loadFours(){
    for(var x1=0;x1<mydata.dynamicData.length;x1++){
        flag++;
        if(flag==5){
            flag=1;
            $('.contents .row').append('<div class="clearfix"></div>');
        }
        $('.contents .row').append('<div class="col-xs-3 col-md-3 col-md-3"><div id="'+x1+'" class="image_title wow bounceIn"><img data-toggle="modal" data-target="#text_modal" src="'+mydata.dynamicData[x1].image+'"/><a href="javascript:void(0)" data-toggle="modal" data-target="#text_modal">'+mydata.dynamicData[x1].sample+'</a></div></div>');
    }
}

function load_Moodle_Data(jsonPosition){
    $('.modal-header').css('display','block');
    
    //moodle header
    if(mydata.dynamicData[jsonPosition].Moodle_header != ''){
        $('#myModalLabel').html(mydata.dynamicData[jsonPosition].Moodle_header);
    }else{
        $('.modal-header').css('display','none');
    }
    if(mydata.dynamicData[jsonPosition].content_type == 'text'){
        $('.modal_text').html('<p>'+mydata.dynamicData[jsonPosition].content_text+'</p>');
    
    }else if(mydata.dynamicData[jsonPosition].content_type == 'textWithImage'){
        $('.modal_text').html('<div class="row"><div class="col-xs-6 col-md-6 col-sm-6"><article><p>'+mydata.dynamicData[jsonPosition].content_text+'</p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><img class="contentpic" src="'+mydata.dynamicData[jsonPosition].mediaSource+'"/></div></div>');

    }else if(mydata.dynamicData[jsonPosition].content_type == 'textWithVideo'){
        $('.modal_text').html('<div class="row"><div class="col-xs-4 col-md-4 col-sm-4"><article><p>'+mydata.dynamicData[jsonPosition].content_text+'</p></article></div><div class="col-xs-8 col-md-8 col-sm-8"><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.mp4" type="video/mp4"/></video></div></div>');
        
    }else if(mydata.dynamicData[jsonPosition].content_type == 'TextWithSlider'){
        $('.modal_text').html('<div class="row"><div class="col-xs-6 col-md-6 col-sm-6"><article><p>'+mydata.dynamicData[jsonPosition].content_text+'</p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><div class="media_cont"></div></div></div>');
        
        loadSlider(jsonPosition); 
        
    }else if(mydata.dynamicData[jsonPosition].content_type == 'slider'){
        $('.modal_text').html('<div class="row"><div class="col-xs-12 col-md-12 col-sm-12"><div class="media_cont"></div></div></div>');
        
        loadSlider(jsonPosition);
        
    }else if(mydata.dynamicData[jsonPosition].content_type == 'video'){
        $('.modal_text').html('<div class="row"><div class="col-xs-12 col-md-12 col-sm-12"><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[jsonPosition].mediaSource+'.mp4" type="video/mp4"/></video></div></div>');
    
    }else if(mydata.dynamicData[jsonPosition].content_type == 'image'){
        $('.modal_text').html('<div class="row"><div class="col-xs-12 col-md-12 col-sm-12"><img style="text-align:center" class="contentpic" src="'+mydata.dynamicData[jsonPosition].mediaSource+'"/></div></div>');
    }
}

//to load slider on screen
function loadSlider(jsonPosition){
    $('.media_cont').append('<div id="carousel" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a class="left carousel-control" href="#carousel" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>');
            
    for(var nn=0;nn<mydata.dynamicData[jsonPosition].mediaSource.length;nn++){
        $('#carousel ol').append('<li data-target="#carousel" data-slide-to="'+nn+'"></li>');
        $('#carousel .carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[jsonPosition].mediaSource[nn]+'"></div>');
    }
    
    $('#carousel li:nth-child(1), #carousel .carousel-inner div:nth-child(1)').addClass('active');    
}


















