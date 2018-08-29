var sound_click='../sound/click';
var narration;
var slide_num=0;
$(function(){
    // this key code to link with player and load json 
  /*  loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Example_and_solution.js', function (a, b) {}).onload = function () {*/
        var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
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

function start(){
    new WOW().init();
    
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
    
    $("#repeat_btn").bind("click",function(e){
        history.go(0);
    });
    
    $('.close_msg').click(function() {
        $(".notification_msg").remove();
    });

    loadData();
    
    //play narration on page ready
    onsliding(slide_num);
    
}

function loadData(){
    //remove slide buttons if there is only one example
    if(mydata.dynamicData.length==1){
        $('#carousel_temp_24 .carousel-control').remove();
//        $('#carousel_temp_24').removeAttr('class');
        $('#carousel_temp_24').removeAttr('data-ride');
        $('#carousel_temp_24').removeAttr('data-interval');
        
    }else{
        //on slider change
        $('#carousel_temp_24').bind('slide.bs.carousel', function (e) {
            //play sound effect
            var snd=document.getElementById('noise');
            canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
            snd.src=sound_click+'.mp3';
            snd.load();
            snd.play();

            var slideTo = $(e.relatedTarget).index();
            slide_num=slideTo;

            //play narration on slide
            onsliding(slide_num);

            //make first tab active on sliding
            tab_content(slide_num,0);
            $('ul li:nth-child(1)').addClass('active');
        });
    }
    //add items depend on number of dynamicData items
        for(var num=0;num<mydata.dynamicData.length;num++){
            var plus=num+1;
            $('#carousel_temp_24 .carousel-inner').append('<div class="item"><div class="temp_box"><div class="temp_info"></div><div class="temp_tab"><ul class="nav nav-tabs" role="tablist"></ul><div class="tab_content"></div></div></div></div>');

            //right box title(red box)
            if(mydata.dynamicData[num].right_hand.title !=''){
                $('#carousel_temp_24 .carousel-inner .item:nth-child('+plus+') .temp_info').append('<h1>'+mydata.dynamicData[num].right_hand.title+'</h1>')
            }
            //right box content
            if(mydata.dynamicData[num].right_hand.content !=''){
                $('#carousel_temp_24 .carousel-inner .item:nth-child('+plus+') .temp_info').append('<h4 class="text-line">'+mydata.dynamicData[num].right_hand.content+'</h4>')
            }

            //left box content
            for(var x=0;x<mydata.dynamicData[num].left_hand.length;x++){
                //add upper buttons on the left box
                $('#carousel_temp_24 .carousel-inner .item:nth-child('+plus+') .temp_box .temp_tab .nav-tabs').append('<li><a>'+mydata.dynamicData[num].left_hand[x].title+'</a></li>');

            }
        }
        
        $('#carousel_temp_24 .carousel-inner .item:nth-child(1)').addClass('active');
        $('#carousel_temp_24 .carousel-inner .item .temp_box .temp_tab .nav-tabs li:nth-child(1)').addClass('active');

        $('#carousel_temp_24 .carousel-inner .item .temp_box .temp_tab .nav-tabs li a, #carousel_temp_24 .carousel-control span').css('cursor','pointer');
        //on click on navgation buttons
        $('#carousel_temp_24 .carousel-inner .item .temp_box .temp_tab .nav-tabs li a, #carousel_temp_24 .carousel-control span').click(function(){
            //stop all videos
            $('video').each(function() {
                $(this).get(0).pause();
            });
            if($('.temp_tab ul li').hasClass('active')){
                $('.temp_tab ul li').removeClass('active');
            }
            $(this).parent().addClass('active');
            tab_content(slide_num,$(this).parent().index());
        });
    
    //add content for first button
    tab_content(0,0);  
}

//change tab content depend on active slide and active button
function tab_content(slide,btn){
    var item_num=slide+1;
    if(mydata.dynamicData[slide].left_hand[btn].content.type == 'text'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<h4 class="text-line">'+mydata.dynamicData[slide].left_hand[btn].content.paragraph+'</h4>');
    
    }
    else if(mydata.dynamicData[slide].left_hand[btn].content.type == 'image'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<div class="col-xs-12 col-md-12 col-sm-12"><img src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource+'"/></video></div>');
    
    }
    else if(mydata.dynamicData[slide].left_hand[btn].content.type == 'video'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<div class="col-xs-12 col-md-12 col-sm-12"><video poster="'+mydata.dynamicData[slide].left_hand[btn].content.poster+'" controls style="width:100%"><source src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource+'.mp4" type="video/mp4"/></video></div>');
    
    }
    else if(mydata.dynamicData[slide].left_hand[btn].content.type == 'textWithImage'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<div class="col-xs-6 col-md-6 col-sm-6"><h4>'+mydata.dynamicData[slide].left_hand[btn].content.paragraph+'</h4></div><div class="col-xs-6 col-md-6 col-sm-6"><img src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource+'" style="'+mydata.dynamicData[slide].left_hand[btn].content.style+'"/></div>');
    
    }
        else if(mydata.dynamicData[slide].left_hand[btn].content.type == 'textWithVideo'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<div class="col-xs-6 col-md-6 col-sm-6"><h4>'+mydata.dynamicData[slide].left_hand[btn].content.paragraph+'</h4></div><div class="col-xs-6 col-md-6 col-sm-6"><video poster="'+mydata.dynamicData[slide].left_hand[btn].content.poster+'" controls style="width:100%"><source src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource+'.mp4" type="video/mp4"/></video></div>');
    
    }
            else if(mydata.dynamicData[slide].left_hand[btn].content.type == 'TextWithSlider'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<div class="col-xs-6 col-md-6 col-sm-6"><h4>'+mydata.dynamicData[slide].left_hand[btn].content.paragraph+'</h4></div><div class="col-xs-6 col-md-6 col-sm-6"><div class="media_cont"><div id="carousel1" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a style="top:0px;left:0px" class="left carousel-control" href="#carousel1" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a style="top:0px;right:0px" class="right carousel-control" href="#carousel1" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div></div></div>');
        
        for(var w=0;w<mydata.dynamicData[slide].left_hand[btn].content.mediaSource.length;w++){
            $('#carousel1 ol').append('<li data-target="#carousel1" data-slide-to="'+w+'"></li>');
            $('#carousel1 .carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource[w]+'" style="'+mydata.dynamicData[slide].left_hand[btn].content.style+'"></div>');
        }
        $('#carousel1 ol li:nth-child(1)').addClass('active');
        $('#carousel1 .carousel-inner div:nth-child(1)').addClass('active');
        
    
    }
          else if(mydata.dynamicData[slide].left_hand[btn].content.type == 'slider'){
        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').html('<div class="col-xs-12 col-md-12 col-sm-12"><div class="media_cont"><div id="carousel1" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a style="top:0px;left:0px" class="left carousel-control" href="#carousel1" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a style="top:0px;right:0px" class="right carousel-control" href="#carousel1" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div></div></div>');
        
        for(var w=0;w<mydata.dynamicData[slide].left_hand[btn].content.mediaSource.length;w++){
            $('#carousel1 ol').append('<li data-target="#carousel1" data-slide-to="'+w+'"></li>');
            $('#carousel1 .carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[slide].left_hand[btn].content.mediaSource[w]+'"></div>');
        }
        $('#carousel1 ol li:nth-child(1)').addClass('active');
        $('#carousel1 .carousel-inner div:nth-child(1)').addClass('active');
        
    
    }
    
    //add example if it is not empty
//    if(mydata.dynamicData[slide].left_hand[btn].example != ''){
//        $('.item:nth-child('+item_num+') .temp_box .temp_tab .tab_content').append('<div class="col-xs-12 col-md-12 col-sm-12"><div class="temp_com"><a class="btn btn-primary btn-lg btn-flat"  data-toggle="collapse" href="#comment"> '+mydata.dynamicData[slide].left_hand[btn].example.title+'</a><span id="comment" class="collapse" data-arrow="&#9658;">'+mydata.dynamicData[slide].left_hand[btn].example.content+'</span></div></div>');
//    }    
    
    if(mydata.dynamicData[slide].left_hand[btn].sub_narration != undefined && mydata.dynamicData[slide].left_hand[btn].sub_narration != ''){
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=mydata.dynamicData[slide].left_hand[btn].sub_narration+'.mp3';
        narration.load();
        narration.play();
        $('#narration').on('ended', function() {
                narration.pause();
        });
    }else{
        narration.pause();
    }
    
}


function play_firstTab_audio(slideTo){
    narration=document.getElementById('narration');
    canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
    narration.src=mydata.dynamicData[slideTo].left_hand[0].sub_narration+'.mp3';
    narration.load();
    narration.play();
    $('#narration').on('ended', function() {
            narration.pause();
    });
}

function onsliding(slideTo){
    //play tab narration
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        if(mydata.dynamicData[slideTo].right_hand.narration != undefined && mydata.dynamicData[slideTo].right_hand.narration != ''){
            
            narration.src=mydata.dynamicData[slideTo].right_hand.narration+'.mp3';
            narration.load();
            narration.play();
            
            $('#narration').on('ended', function() { 
                play_firstTab_audio(slide_num);
            });
            
        }else{
            play_firstTab_audio(slide_num);
        }
}