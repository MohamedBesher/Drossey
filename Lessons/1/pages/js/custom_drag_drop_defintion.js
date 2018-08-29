var correctCards = 0;
var cardsCount,itemsData;
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
};
function start(pageNum) {
    // this key code to link with player and load json 
     /*   loadScript('../Player/js/redundant.js', function (a, b) {}).onload = function () {
        loadScript('../Player/lessons/' + lessonID + '/media/data_pages_'+currFileName()+'.js', function (a, b) {}).onload = function () {*/
             //read from dynamic script 
    var jsonFile = sessionStorage.getItem('jsonFile');
    var lesson_id=  sessionStorage.getItem('lessonId');
   
         loadScript("../Data/Lessons/"+lesson_id+"/"+jsonFile+".js",function (a, b) {}).onload = function ()
        {
            init(pageNum);
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
//console.log(pages[pageNum].data_gallary[0].definition)
    // define dg as data array for gallary from json 
   itemsData=pages[pageNum].items_data;
    cardsCount=itemsData.length;
//    localStorage.setItem("cardsCount", itemsData.length);
    //console.log(itemsData.length)
    for (var i=0;i<itemsData.length;i++){
        answers.push({x:i+1, y:itemsData[i].word});
    }  
		
  answers.sort( function() { return Math.random() - .5; } );
 
for ( var i=0; i<answers.length; i++ ) {
      
    $('<div class="grid-xs-4.5 grid-md-3"><div class="card_number">' + answers[i].y + '</div></div>').appendTo( '#cardPile' ).children(".card_number").data( 'number', answers[i].x ).draggable( {
      containment: '.cards_content',
        start:function(){ 
                    playAudio(dragSoundPath);
            },
      stack: '#cardPile .card_number',
      cursor: 'move',
      revert: true
    } );
  }                                 
                                         
            $('<div class="clearfix"></div>').appendTo( '#cardPile' ) ;                   
  // Create the card slots
	
  for ( var i=1; i<=itemsData.length; i++ ) {
    $('<div class="grid-xs-2.5 grid-md-3"><div class="drop-place"><div class="card_word"></div><p class="card-desc">' + itemsData[i-1].definition + '</p></div></div>').appendTo( '#cardSlots' ).children().children(".card_word").data( 'number', i ).droppable( {     
        accept: '#cardPile .card_number',
      hoverClass: 'hovered',
      drop: handleCardDrop
    } );
  }
  $('<div class="clearfix"></div>').appendTo( '#cardSlots' )   ;   
}
 //====================================
function handleCardDrop( event, ui ) {
   var slotNumber = $(this).data( 'number' );
  var cardNumber = ui.draggable.data( 'number' );
  // If the card was dropped to the correct slot,
  // change the card colour, position it directly
  // on top of the slot, and prevent it being dragged
  // again
 //subort sound
if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
 if ( slotNumber == cardNumber ) {
     $(this).html($(ui.draggable).html());
     ui.draggable.draggable( 'disable' );
     $(this).droppable( 'disable' );
     ui.draggable.remove(); 
     $(this).addClass( 'correct' );
     correctCards++;
     playAudio(dropSoundPath);
     if(itemsData[cardNumber-1].sub_narration!=undefined){
         //subort sound
         if(audioTap!=undefined){ if(audioTap[0]!=undefined  )audioTap[0].pause()}
         playTapAudio(itemsData[cardNumber-1].sub_narration)
     }
  } 
   
   
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
    $(".contents").addClass('temp_drag_drop_defintion');
    if(pages[pageNum].article!=undefined){
        setParagraph(".contents");
        if(pages[pageNum].article.title!=undefined ){  
            $('.page_title').html(pages[pageNum].article.title);
        }else{$('.page_title').remove();}
        $('.page_desc').html(pages[pageNum].article.text);
    }
         setCardsDragROW('.contents');
        setCorDeclar();
    if(pages[pageNum].tip_msg!=undefined){
        setNotiMsg("#main_container",pages[pageNum].tip_msg);
     }
    if(pages[pageNum].narration!=undefined ){
        if(pages[pageNum].narration!=""){playTapAudio(pages[pageNum].narration);}    
    }   
}
function setCardsDragROW(location){
   $(location).append('<div class="cards_content "><div class="row"><div id="cardPile"  ></div></div><div class="row"><div id="cardSlots"></div></div><div class="clearfix"></div></div><!--end fliping_content-->');
}
function setCorDeclar(){
    $('.container').after('<!--=========Modal=========--><div class="modal fade bs-example-modal-sm" id="card_modal" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"><div class="modal-dialog modal-sm"><div class="modal-content"><div class="modal-header"><button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button><h4 class="modal-title" id="myModalLabel">تأكيد</h4></div><div class="modal-body"><div class="alert alert-success" style="text-align:center;"> أحسنت إجابة صحيحة. </br>  تمت جميع الإختيارات بنجاح.   </div></div></div></div></div><!--/////////////////////////////-->');
    
}

