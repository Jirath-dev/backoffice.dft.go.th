/*

      Internet Explorer Browser detector v0.1.2
      By Eric Gerds   http://www.pinlady.net/PluginDetect/
      

 Note:
  1) Place this script in the <head> or <body> of your web page.
  2) The output <div>, if you choose to use one, should be placed in the <body> tag,
  and before this script. The output <div> looks like this:
       <div id="detectIE_output"></div>
  3) Feel free to modify/minify this script as you wish.

*/



// "use strict";


// Add text to output node
var docWrite = function(text){
   var out = document.getElementById("detectIE_output");  // node for output text
   if (out){
        if (text){
          text = text.replace(/&nbsp;/g,"\u00a0");
          out.appendChild(document.createTextNode(text));
        };
        out.appendChild(document.createElement("br"));
   }
   else document.write(text + "<br>");
};  // end of function


(function(){

 var brwsr = {   // browser object

     // Determine if browser is IE.
     // Based on  http://dean.edwards.name/weblog/2007/03/sniff/
     // [Boolean]
     isIE:eval("/*@cc_on!@*/!1"),


     // Version of IE
     // [number / null]
     verIE:null,


     // brwsr.docModeIE is essentially the document mode for the IE browser.
     // It is the EFFECTIVE version of IE that renders the web page.
     // This tells you the level of HTML/CSS/Image support in IE.
     // It is independent of the user Agent, usually.
     // [number/null]
     //
     // If brwsr.docModeIE >= 6 for IE Desktop,
     //    then we have IE 6+ in Standards Mode.
     //
     // If brwsr.docModeIE == 5 for IE Desktop,
     //    then we have IE 6+ in Quirks mode,
     //
     // Note: For IE 5.5, brwsr.docModeIE == 5.5
     // Note: For IE 5, brwsr.docModeIE == 5
     //
     // No one uses IE < 6 anymore, so brwsr.docModeIE == 5 means that 
     // IE version >= 6 in Quirks Mode.
     docModeIE:null,
     
     
     // FULL Version of IE, derived from clientCaps.
     // Independent of browser mode / document mode / navigator.userAgent
     // [string "AA.BB.CCCC.DDDDD" / null]
     verIEfull:null,


     // Version of IE, derived from navigator.userAgent
     // [number/null]
     //
     // Used as a backup, in case all other detection methods fail.
     verIE_ua:null


  };  // end of object




  // We only let IE run this code.
  if (brwsr.isIE){
    
    // Version from user Agent
    brwsr.verIE_ua = (/MSIE\s*(\d+\.?\d*)/i).test(navigator.userAgent || "") ?
       parseFloat(RegExp.$1, 10) : null;


    // Get brwsr.verIEfull using clientCaps.
    var e, verFullFloat,
      obj = document.createElement("div");

    try{

      obj.style.behavior = "url(#default#clientcaps)";

      // This works for IE 5.5+.
      // For IE 5, we would need to insert obj into the DOM, then set the behaviour,
      // and then query.
      brwsr.verIEfull = obj.getComponentVersion("{89820200-ECBD-11CF-8B85-00AA005B4383}",
          "componentid").replace(/,/g,".");

    }catch(e){};


    // Given string "AA.BB.CCCC.DDDD", convert to a floating point number AA.BB
    // If verIEfull is null, then verFullFloat is 0.
    verFullFloat = parseFloat(brwsr.verIEfull||"0", 10);



    // For IE 8+, we look at document.documentMode
    //
    // Note: It is unlikely that a web designer would set document.documentMode to
    // some arbitrary value for IE < 8.
    brwsr.docModeIE = document.documentMode ||


       // If document.documentMode is not defined, then we have IE < 8 Desktop.
       // We try to artificially create a document mode number.
       //
       // if document.compatMode == "BackCompat", then we have Quirks mode, so return 5.
       // document.documentMode == 5 in Quirks mode.
       ((/back/i).test(document.compatMode || "") ? 5 : verFullFloat) ||

       // Else return version from navigator.userAgent, or null
       brwsr.verIE_ua;



    // [number / null]
    //
    // Try to use clientcaps version first, because this gives the
    // actual browser version.
    //
    // If not available, then use the document mode.
    // In most cases, this will be the actual IE version, anyway.
    brwsr.verIE = verFullFloat || brwsr.docModeIE;


};
  
  
  
  docWrite("Your browser is IE: " + brwsr.isIE);
  docWrite("Your IE browser version: " + brwsr.verIE);
  docWrite("Your IE browser document mode: " + brwsr.docModeIE);

  docWrite("----------------------------------------------------------------------------");

  docWrite("Full IE browser version, derived from clientCaps: " + brwsr.verIEfull);
  docWrite("IE browser version, derived from navigator.userAgent: " + brwsr.verIE_ua);



})();   // end of function




// ---------------------------------------------------------------------------------------------
// The code below this line is not needed for our IE browser detection.
// It is included here merely for demonstration purposes.


/*
(function(){


  // Get version of IE, derived from Conditional Comments.
  // [number / null]
  //
  // code is based on:
  //       https://gist.github.com/527683
  //       http://james.padolsey.com/javascript/detect-ie-in-js-using-conditional-comments/
  //
  // However, this really only works for IE < 10.
  // IE 10+ in Standards Mode does not support conditional comments.
  //
  // However, IE 10 in quirks mode does support conditional comments
  //    (where documentMode==5 in quirks mode).
  // Will IE 11 and higher support conditional comments in quirks mode?
  //
  // See http://blogs.msdn.com/b/ie/archive/2011/07/06/html5-parsing-in-ie10.aspx
  // Note: IE 10 in standards mode does not support conditional comments.
  //
  //
  var div = document.createElement("div"),
        all = div.getElementsByTagName("i"), verCC = 4;
  while(

        // In case this code is inline Javascript, we break up the string
        // so the browser's HTML parser does not see any of this code
        // as HTML.
        div.innerHTML = "<!--[if gte IE " + (++verCC) + "]><i></i><![endif]-->",

        all[0]

  ){};    // end of while loop

  verCC = verCC >= 6 ? verCC-1 : null;


  docWrite("IE browser version, derived from IE conditional comments: " + verCC);



})();   // end of function
*/



