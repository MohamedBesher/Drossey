var sound_click ='../sound/click';

var narration;
$(function(){
    // this key code to link with player and load json 
  /*  loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/Flowchart_Up.js', function (a, b) {}).onload = function () { */
         var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
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
    
//    $("#repeat_btn").bind("click",function(e){
//        history.go(0);
//    });
//    
    $('.close_msg').click(function(){
        $('.notification_msg').remove();        
    });
    
    //narration on page start
    if(mydata.staticData.narration != undefined && mydata.staticData.narration !=''){
        narration=document.getElementById('narration');
        canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
        narration.src=mydata.staticData.narration+'.mp3';
        narration.load();
        narration.play();
        
        $('#narration').on('ended', function() {
            play_firstTab_audio();
        });
    }else{
        play_firstTab_audio();
    }
    
    loadTabContent(0);//load first tab content
    loadTabs();
}

//load tab buttons
function loadTabs(){
    //flow chart title
    $('.temp_25_chart h1').html(mydata.main_title); 
    //load tabs buttons
    for(var n=0; n<mydata.dynamicData.length; n++){
        $('.temp_25_chart ul').append('<li><a href="#">'+mydata.dynamicData[n].title+'</a></li>');
    }
    $('ul li:nth-child(1)').addClass('active');
    
    //change tab content depending on clicked tab
    $('ul li').click(function(){
        //play sound effect
        var snd=document.getElementById('noise');
        canPlayMP3 = (typeof snd.canPlayType === "function" && snd.canPlayType("audio/mpeg") !== "");
        snd.src=sound_click+'.mp3';
        snd.load();
        snd.play();
        
        //play tab narration
        if(mydata.dynamicData[$(this).index()].sub_narration != undefined && mydata.dynamicData[$(this).index()].sub_narration != ''){
            narration=document.getElementById('narration');
            canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
            narration.src=mydata.dynamicData[$(this).index()].sub_narration+'.mp3';
            narration.load();
            narration.play();
            
            $('#narration').on('ended', function() {
                narration.pause();
            });
            
        }else{
            narration.pause();
        }
        
        if($('ul li').hasClass('active')){
            $('ul li').removeClass('active');
        } 
        $(this).addClass('active');
        loadTabContent($(this).index());
    });    
}

var id=[];                  
function loadTabContent(num){
    if(mydata.dynamicData[num].type=='text'){
        
       $('.blue_bg .row').html('');
         $(id).each(function(i,v){
            clearTimeout(v);
        });
        
          $(mydata.dynamicData[num].paragraph).each(function(index,value){
             if(index==0)
                id.push(setTimeout(function(){ $('.blue_bg .row ').append('<div class="col-md-12 col-sm-12 col-xs-12">'+value.text+'</div>');},value.time));
            else
                 id.push(setTimeout(function(){ $('.blue_bg .row').append(value.text);},value.time));
                
        });
        
//        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12"><h4 class="text-line animated bounceInDown ">'+mydata.dynamicData[num].paragraph+'</h4></div>');
    
    }else if(mydata.dynamicData[num].type=='textWithImage'){
        
         $('.blue_bg .row').html('');
        //clear all time out
       $(id).each(function(i,v){
            clearTimeout(v);
        });
        $('.blue_bg .row').append('<div id="articalContant" class="col-md-8 col-sm-8 col-xs-12"></div>');
        $(mydata.dynamicData[num].paragraph).each(function(index,value){
             if(index==0)
                id.push(setTimeout(function(){ $('#articalContant').append('<article>'+value.text+'</article>');},value.time));
         
            else
                 id.push(setTimeout(function(){ $('.blue_bg .row article').append(value.text);},value.time));
                
        });
        
        id.push(setTimeout(function(){ $('.blue_bg .row').append('<div class="col-md-4 col-sm-4 col-xs-12 text-center"><img src="'+mydata.dynamicData[num].mediaSource.src+'" class="animated zoomIn" style="'+mydata.dynamicData[num].style+'" /></div>');},mydata.dynamicData[num].mediaSource.time));
        
//        $('.blue_bg .row').html('<div class="col-md-8 col-sm-8 col-xs-8"><h4 class="text-line animated bounceInDown ">'+mydata.dynamicData[num].paragraph+'</h4></div><div class="col-md-4 col-sm-4 col-xs-4 text-center" ><img src="'+mydata.dynamicData[num].mediaSource+'" class="animated zoomIn"/></div>');
    
        
        
    }else if(mydata.dynamicData[num].type=='textWithVideo'){
        
         $('.blue_bg .row').html('');
        //clear all time out
       $(id).each(function(i,v){
            clearTimeout(v);
        });
        $('.blue_bg .row').append('<div id="articalContant" class="col-xs-12 col-md-6 col-sm-6"></div>');
        $(mydata.dynamicData[num].paragraph).each(function(index,value){
             if(index==0)
                id.push(setTimeout(function(){ $('.blue_bg .row #articalContant').append('<article>'+value.text+'</article>');},value.time));
            else
                 id.push(setTimeout(function(){ $('.blue_bg .row article').append(value.text);},value.time));
                
        });
        
          id.push(setTimeout(function(){ $('.blue_bg .row').append('<div class="col-xs-12 col-md-6 col-sm-6  text-center"><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[num].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[num].mediaSource+'.mp4" type="video/mp4"/></video</div>');},mydata.dynamicData[num].mediaSource.time));
        
     
        
        
//        $('.blue_bg .row').html('<div class="col-md-6 col-sm-6 col-xs-6"><h4 class="text-line animated bounceInDown ">'+mydata.dynamicData[num].paragraph+'</h4></div><div class="col-md-6 col-sm-6 col-xs-6 text-center" ><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[num].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[num].mediaSource+'.mp4" type="video/mp4"/></video></div>');
//    
    }else if(mydata.dynamicData[num].type=='TextWithSlider'){
        
         $('.blue_bg .row').html('');
        //clear all time out
        $(id).each(function(i,v){
            clearTimeout(v);
        });
        $('.blue_bg .row').append('<div id="articalContant" class="col-md-8 col-sm-8 col-xs-12"></div>');
         $(mydata.dynamicData[num].paragraph).each(function(index,value){
             if(index==0)
                id.push(setTimeout(function(){ $('.blue_bg .row  #articalContant').append('<article>'+value.text+'</article>');},value.time));
            else
                 id.push(setTimeout(function(){ $('.blue_bg .row article').append(value.text);},value.time));
                
        });
        $('.blue_bg .row').append('<div class="col-md-4 col-sm-4 col-xs-12 text-center"><div class="media_cont"></div></div>');
        
        
//        $('.blue_bg .row').html('<div class="col-md-8 col-sm-8 col-xs-8"><h4 class="text-line animated bounceInDown ">'+mydata.dynamicData[num].paragraph+'</h4></div><div class="col-md-4 col-sm-4 col-xs-4 text-center" ><div class="media_cont"></div></div>');
        
        loadSlider(num);
    
    }else if(mydata.dynamicData[num].type=='slider'){
        $('.blue_bg .row').html('');
        //clear all time out
        $(id).each(function(i,v){
            clearTimeout(v);
        });
        $('.blue_bg .row').append('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><div class="media_cont"></div></div>');
        
        loadSlider(num)
        
        
//        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><div class="media_cont"></div></div>');
//        
//        loadSlider(num);
        
    }else if(mydata.dynamicData[num].type=='video'){
        
        $('.blue_bg .row').html('');
        //clear all time out
        $(id).each(function(i,v){
            clearTimeout(v);
        });
        
        $('.blue_bg .row').append('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[num].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[num].mediaSource+'.mp4" type="video/mp4"/></video></div>');
        
        
        
//        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><video autoplay controls style="width:100%"><source src="'+mydata.dynamicData[num].mediaSource+'.ogv" type="video/ogg"/><source src="'+mydata.dynamicData[num].mediaSource+'.mp4" type="video/mp4"/></video></div>');
    
    }else if(mydata.dynamicData[num].type=='image'){
          $('.blue_bg .row').html('');
             $(id).each(function(i,v){
            clearTimeout(v);
        })
        id.push(setTimeout(function(){  $('.blue_bg .row').append('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><img src="'+mydata.dynamicData[num].mediaSource.src+'" class="animated zoomIn" style="'+mydata.dynamicData[num].style+'"/></div>');}, mydata.dynamicData[num].mediaSource.time));
        
//        $('.blue_bg .row').html('<div class="col-md-12 col-sm-12 col-xs-12 text-center" ><img src="'+mydata.dynamicData[num].mediaSource+'" class="animated zoomIn"/></div>');
      
    }    
}

//to load slider on screen
function loadSlider(newn){
    $('.media_cont').append('<div id="carousel" class="carousel slide" data-ride="carousel"><ol class="carousel-indicators"></ol><div class="carousel-inner" role="listbox"></div><a class="left carousel-control" href="#carousel" role="button" data-slide="prev"><span class="fa fa-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span></a><a class="right carousel-control" href="#carousel" role="button" data-slide="next"><span class="fa fa-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span></a></div>');
            
    for(var nn=0;nn<mydata.dynamicData[newn].mediaSource.length;nn++){
        $('.carousel ol').append('<li data-target="#carousel" data-slide-to="'+nn+'"></li>');
        $('.carousel .carousel-inner').append('<div class="item"><img src="'+mydata.dynamicData[newn].mediaSource[nn]+'" style="'+mydata.dynamicData[newn].style+'" ></div>');
    }
    
    $('.carousel li:nth-child(1), .carousel .carousel-inner div:nth-child(1)').addClass('active');    
}

function play_firstTab_audio(){
    if(mydata.dynamicData[0].sub_narration != undefined && mydata.dynamicData[0].sub_narration != ''){
    narration=document.getElementById('narration');
    canPlayMP3 = (typeof narration.canPlayType === "function" && narration.canPlayType("audio/mpeg") !== "");
    narration.src=mydata.dynamicData[0].sub_narration+'.mp3';
    narration.load();
    narration.play();
    $('#narration').on('ended', function() {
            narration.pause();
    });
    }
    else{
        narration.pause();
    }
}