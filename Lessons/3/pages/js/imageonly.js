   var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
var sound_click ='../sound/click';

var narration;
$(document).ready(function() {
    // this key code to link with player and load json 
        /*loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Roll_over_words.js', function (a, b) {}).onload = function () {*/


   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            start();
        };
    //}
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
        $('.ttitle').prepend('<p class="page_desc text-justify">'+mydata.staticData.inside_title+'</p>');
    }
    //page title
    if(mydata.staticData.title !='' && mydata.staticData.title != undefined){
        $('.ttitle').prepend('<h2 class="tt title_font page_title">'+mydata.staticData.title+'</h2>');
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
		$(".notification_msg").remove();
	});
    
    load_tooltip_content();    
}

//load data and it's hover text on screen
function load_tooltip_content(){
    //test if content is text only or text with media
    if(mydata.mediaType=='text'){
        $('.contents .row').append('<div class="col-xs-12 col-md-12 col-sm-12"><article><p></p></article></div>');
    }else if(mydata.mediaType=='text_image'){
        $('.contents .row').append('<div class="media_cont col-xs-12 col-md-8 col-md-offset-2 " style=" text-align:center" ><img src="'+mydata.mediaSource+'" alt=""/></div>');
    }else if(mydata.mediaType=='text_video'){
        $('.contents .row').append('<div class="col-xs-6 col-md-6 col-sm-6"><article><p></p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><video controls autoplay style="width:100%"><source src="'+mydata.mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.mediaSource+'.mp4" type="video/mp4"/></video></div>');
    }else if(mydata.mediaType=='text_slider'){
        $('.contents .row').append('<div class="col-xs-6 col-md-6 col-sm-6"><article><p></p></article></div><div class="col-xs-6 col-md-6 col-sm-6"><div class="media_cont"></div></div>');
        
        $('.media_cont').append('<div id="carousel" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a class="left carousel-control" href="#carousel" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>');
            
        for(var nn=0;nn<mydata.mediaSource.length;nn++){
            $('.carousel ol').append('<li data-target="#carousel" data-slide-to="'+nn+'"></li>');
            $('.carousel .carousel-inner').append('<div class="item"><img src="'+mydata.mediaSource[nn]+'"></div>');
        }    
        $('.carousel li:nth-child(1), .carousel .carousel-inner div:nth-child(1)').addClass('active');
    }
    var test ='';
    var pp=mydata.paragraph;
    //loop on whole paragraph
    for(var x=0;x<pp.length;x++){
        //loop on tooltip words
        var flag=0;
        for(var x2=0;x2<mydata.dynamicData.length;x2++){            
            if(pp.substr(x,mydata.dynamicData[x2].word.length) == mydata.dynamicData[x2].word){
                test+='<span class="badge" data-toggle="tooltip" data-placement="top" title="'+mydata.dynamicData[x2].definition+'">'+pp.substr(x,mydata.dynamicData[x2].word.length)+'</span>';
                $('article p').append('<span class="badge" data-toggle="tooltip" data-placement="top" title="'+mydata.dynamicData[x2].definition+'">'+pp.substr(x,mydata.dynamicData[x2].word.length)+'</span>')
                x+=mydata.dynamicData[x2].word.length-1;
            }else{
                flag++;
                if(flag==mydata.dynamicData.length){
                    test+=pp.slice(x,x+1);
                    $('article p').append(pp.slice(x,x+1))
                }
            }           
        }
    }
    $('article p').html(test);
    $('[data-toggle="tooltip"]').tooltip();    
    
    //on mouse move over tooltip
    $('.badge').mouseover(function(){
        //play sound effect
        var snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=sound_click+'.mp3';
        snd.load();
        snd.play();
        
        //to play sub_narration on mouse move over tooltip
        for(var x=0; x<mydata.dynamicData.length;x++){
            if(mydata.dynamicData[x].word == $(this).text() && mydata.dynamicData[x].sub_narration != ''){
                narration=document.getElementById('narration');
                canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
                narration.src=mydata.dynamicData[x].sub_narration+'.mp3';
                narration.load();
                narration.play();
                break;
            }else{
                narration.pause();
            }
        }
    });
    
    $('.badge').mouseleave(function(){
        narration.pause();
    });
}