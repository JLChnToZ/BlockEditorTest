var editor, JSOnly = false, modeSwitching = false;

if(Blockly && Blockly.JavaScript) { // Override for Custom JavaScript Framework
  Blockly.JavaScript.text_print = function (block) {
    return "printLine(" + (Blockly.JavaScript.valueToCode(block, "TEXT", Blockly.JavaScript.ORDER_NONE) || "''") + ");\n"
  };
  Blockly.JavaScript.text_prompt = function (block) {
    var output = "prompt(" + Blockly.JavaScript.quote_(block.getFieldValue("TEXT")) + ")";
    "NUMBER" == block.getFieldValue("TYPE") && (output = "parseFloat(" + output + ")");
    return [output, Blockly.JavaScript.ORDER_FUNCTION_CALL]
  };
}

$(function() {
  ace.config.set("workerPath", "js/ace");
  $("#menu1").text(menu1);
  $("#menu2").text(menu2);
  $("#tablist").tabs({
    beforeActivate: function(e, ui) {
      if(ui.newPanel.is("#scriptedit-tab")) {
        modeSwitching = true;
        editor.resize();
        editor.setValue(Blockly.JavaScript.workspaceToCode());
        editor.clearSelection();
        modeSwitching = false;
      }
    },
    hide: {
      effect: "fadeOut",
      duration: 200
    },
    show: {
      effect: "fadeIn",
      duration: 200
    }
  }).fadeIn("slow");
  Blockly.inject($("#blockedit")[0], { path: './res/', toolbox: blocklytoolbox });
  editor = ace.edit("scriptedit");
  var session = editor.getSession();
  editor.setTheme("ace/theme/tomorrow");
  editor.setShowPrintMargin(false);
  session.setUseSoftTabs(true);
  session.setTabSize(2);
  session.setUseWrapMode(true);
  session.setMode("ace/mode/javascript");
  session.on("change", function(e) {
    if(!modeSwitching) {
      $("#tablist").tabs("disable", 0);
      JSOnly = true;
    }
  });
  $(document).on("contextmenu", function(e) {
    if(!$(e.target).not(".ace_gutter *").is("#scriptedit *, input, textarea"))
      return false;
  });
  if(win && win.drag) {
    var isMouseDown;
    $(".ui-tabs-nav").mousedown(function(e) {
      isMouseDown = true;
    }).mousemove(function(e) {
      if(isMouseDown) win.drag();
    }).mouseup(function(e) {
      isMouseDown = false;
    });
  }
});

function resetAll() {
  modeSwitching = true;
  $("#tablist").tabs("enable", 0);
  Blockly.mainWorkspace.clear();
  editor.setValue("");
  editor.clearSelection();
  JSOnly = false;
  modeSwitching = false;
}

function loadXMLFile(content) {
  modeSwitching = true;
  $("#tablist").tabs("option", "active", 0);
  Blockly.Xml.domToWorkspace(Blockly.mainWorkspace, Blockly.Xml.textToDom(content));
  editor.setValue(Blockly.JavaScript.workspaceToCode());
  editor.clearSelection();
  modeSwitching = false;
}

function loadJSFile(content) {
  $("#tablist").tabs("disable", 0).tabs("option", "active", 1);
  editor.setValue(content);
  editor.clearSelection();
  JSOnly = true;
}

function saveFile() {
  if(!win) return;
  win.setData(
    JSOnly ? editor.getValue() : Blockly.JavaScript.workspaceToCode(),
    JSOnly ? "" : Blockly.Xml.domToText(Blockly.Xml.workspaceToDom(Blockly.mainWorkspace)));
}

function run() {
  eval(JSOnly ? editor.getValue() : Blockly.JavaScript.workspaceToCode());
}

