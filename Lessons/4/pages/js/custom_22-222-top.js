var answerData=[],correctCards=0,cardsCount=0;
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
function start(pageNum) {
  
    // this key code to link with player and load json 
      /*  loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
           
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
    
     var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum);
        };
  //  }
//end key code 
    
    
    }
function init(pageNum) {
   
    answerData=pages[pageNum].data_quiz
   
      loadHTMLTags(pageNum);
     var randomAnswerData=[];
     cardsCount = answerData.length
     //console.log(cardsCount)

    for (var i=0;i<answerData.length;i++){
        randomAnswerData.push({x:i, co:answerData[i]});
    }  
		
    randomAnswerData.sort( function() { return Math.random() - .5 } );
 
    $.each(randomAnswerData,function(index,value){
       // console.log(value.x) 
       
        $('<a href="#" class="col-md-2 col-sm-2 col-xs-3">'+value.co.answer+'</a>').data( 'number', value.x ).appendTo("nav.temp_22_nav").draggable({
            //containment: ".contents",
            start:function(){ 
                    playAudio(dragSoundPath)
            },
            zIndex: 10,
            stack: 'nav.temp_22_nav a',
            cursor: 'move',
            revert: true ,
        })
       
    })
    $.each(answerData,function(index,value){ 
        var className="";
    if($.trim(value.direction)=="left"){className='pos_left'}
           $('<span style="top:'+value.y+'px;right:'+value.x+'px;" class=" drop-text '+className+'">------</span>').data( 'number', index ).appendTo(".science_temp").droppable( {     
               accept: 'nav.temp_22_nav a',
               hoverClass: 'hovered',
               drop: handleCardDrop
           } );
    })
}
function handleCardDrop( event, ui){

     var slotNumber = $(this).data( 'number' );
  var cardNumber = ui.draggable.data( 'number' );
   if ( slotNumber == cardNumber ) {
    ui.draggable.addClass( 'correct' );
    ui.draggable.draggable( 'disable' );
    $(this).droppable( 'disable' );
       $(this).text(ui.draggable.text()) 
       ui.draggable.hide()
//    ui.draggable.position( { of: $(this), my: 'left top', at: 'left top' } );
    ui.draggable.draggable( 'option', 'revert', false );
    correctCards++;
       playAudio(dropSoundPath)
       
       
  } 
    
    if ( correctCards == cardsCount ) {
         playAudio(cAnswerSoundPath)
    $('#card_modal').modal('show');
  }
    
}
 //====================================

function loadHTMLTags(pageNum) {
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_22')
   if(pages[pageNum].article!=undefined){
        setParagraph(".contents")
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);}else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);
        
    }
/*    $('<div class="row"> <div class="col-md-3 col-sm-3 col-xs-3">  <nav class="temp_22_nav">  </nav></div><!--end col-xs-12--> <div class="col-md-9 col-sm-9 col-xs-9"> <div class="blue_bg"> <div class="science_temp"> <img src="'+pages[pageNum].main_img+'"/></div> </div><!--end blue_bg--> </div><!--end col-xs-12--> </div><!--end row-->').appendTo('.contents')*/
    
    
    $('<div class="row"> <nav class="temp_22_nav">  </nav></div><div class="row"> <div class="blue_bg col-md-12 col-sm-12 col-xs-12"> <div class="science_temp"> <img src="'+pages[pageNum].main_img+'"/></div> </div><!--end blue_bg--> </div><!--end col-xs-12--> </div><!--end row-->').appendTo('.contents')
    
    setCorDeclar()
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration)}    
    } 
     
   
}
function setCorDeclar(){
    $('#main_container').after('<!--=========Modal=========--><div class="modal fade bs-example-modal-sm" id="card_modal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h4 class="modal-title" id="myModalLabel">تأكيد</h4></div><div class="modal-body"><div class="alert alert-success" style="text-align:center;"> أحسنت إجابة صحيحة. </br> تمت جميع الإختيارات بنجاح.   </div></div></div></div></div><!--/////////////////////////////-->')
    
}
//refresh page when resize
$(window).on('resize',function(){location.reload();});