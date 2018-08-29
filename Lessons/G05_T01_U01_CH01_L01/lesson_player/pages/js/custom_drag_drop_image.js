var correctCards = 0;
var cardsCount, DQ;
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
    /*    loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
                  //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {   init(pageNum);
        };
   // }
//end key code 
    
    
    }
 
function init(pageNum) {
    loadHTMLTags(pageNum);
  // Reset the game
  correctCards = 0;
   $('#cardPile').html( '' );
  $('#cardSlots').html( '' );
 
  // Create the pile of shuffled cards
  var answers = [ ];
//console.log(pages[pageNum].data_gallary[0].co_answer)
    // define dg as data array for gallary from json 
      DQ=pages[pageNum].data_quiz;
    cardsCount=DQ.length;
//    localStorage.setItem("cardsCount", DQ.length);
    //console.log(DQ.length)
    for (var i=0;i<DQ.length;i++){
        answers.push({x:i+1, y:DQ[i].img_src});
    }  
		
  answers.sort( function() { return Math.random() - .5 } );
 
  for ( var i=0; i<answers.length; i++ ) {
      
    $('<div class="grid-xs-5"><div class="card_img"><img src="' + answers[i].y + '" alt=""/></div></div>').appendTo( '#cardPile' ).children(".card_img").data( 'number', answers[i].x ).draggable( {
        containment: '.img_drag',
        start:function(){
             playAudio(dragSoundPath)
        },
        stack: '#cardPile .card_img ',
        cursor: 'move',
        revert: true
    } );
  }                                 
  // Create the card slots
  for ( var i=1; i<=DQ.length; i++ ) {
    $('<div class="grid-xs-3 " ><div class="drop-place"><div class="row"><div class="col-xs-6 col-sm-6 col-md-6"><div class="card_drop"></div></div><div class="col-xs-6 col-sm-6 col-md-6"><div class="card-desc ">'+ DQ[i-1].question + '</div></div></div></div></div>').data( 'number', i ).appendTo( '#cardSlots' ).droppable( {     
       accept: '#cardPile .card_img',
      hoverClass: 'hovered',
      drop: handleCardDrop
    } );
  }
 
}
 //====================================
function handleCardDrop( event, ui ) {
   var slotNumber = $(this).data( 'number' );
  var cardNumber = ui.draggable.data( 'number' );
  // If the card was dropped to the correct slot,
  // change the card colour, position it directly
  // on top of the slot, and prevent it being dragged
  // again
 if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
 if ( slotNumber == cardNumber ) {
     $(this).find('.card_drop').addClass( 'correct' );
     ui.draggable.draggable( 'disable' );
     $(this).droppable( 'disable' );
     //    ui.draggable.position( { of: $(this).find(".card_drop"), my: 'left top', at: 'left top' } );
     //   ui.draggable.draggable( 'option', 'revert', false );
     ui.draggable.remove()
     $(this).find('.card_drop').html($(ui.draggable).html())
     correctCards++;
     playAudio(dropSoundPath)
     if(DQ[cardNumber-1].sub_narration!=undefined){
         //subort sound
         if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
         playTapAudio(DQ[cardNumber-1].sub_narration)
     }
     
 } 
      
    /* ui.draggable.position( ($(this).find(".card_drop").position().left),($(this).find(".card_drop").position().left) );*/
    // If all the cards have been placed correctly then display a message
    // and reset the cards for another go
    
    if ( correctCards == cardsCount ) {
        $('#card_modal').modal('show');
        playAudio (cAnswerSoundPath);
    }
 
}
function loadHTMLTags(pageNum) {
    
    
    $('title').text(pages[pageNum].name);
    if(pages[pageNum].title!=undefined){
        setNewHeader("body");
        $('#lesson_title').text(pages[pageNum].title);
    }
    setMainContainer("body");
    setMainContent('#main_container'); 
    $(".contents").addClass('temp_drag_drop_image')
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents")
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);
        }else{$('.page_title').remove()}
        $('.page_desc').html(pages[pageNum].article.text);
    }
         setImgDragROW('.contents');
        setCorDeclar()
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration)}    
    }   
    
    
    
    
    
}
function setCorDeclar(){
    $('.container').after('<!--=========Modal=========--><div class="modal fade bs-example-modal-sm" id="card_modal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h4 class="modal-title" id="myModalLabel">تأكيد</h4></div><div class="modal-body"><div class="alert alert-success" style="text-align:center;"> أحسنت إجابة صحيحة. </br>  تمت جميع الإختيارات بنجاح.   </div></div></div></div></div><!--/////////////////////////////-->')
    
}

