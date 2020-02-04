﻿using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace QiqqaLegacyFileFormats          // namespace Qiqqa.Expedition
{
    [Serializable]
    public class ExpeditionDataSource
    {
        public DateTime date_created;

        public List<string> words;
        public Dictionary<string, int> words_index;

        public List<string> docs;
        public Dictionary<string, int> docs_index;

        public int[][] words_in_docs; // jagged array [doc][word]

        public LDASampler lda_sampler;

        [NonSerialized]
        private LDAAnalysis lda_analysis = null;

        public LDAAnalysis LDAAnalysis
        {
            get
            {
                if (null == lda_analysis)
                {
                    // TODO: Analyse code flow and find out if we can DELAY-LOAD expedition and brainstorm
                    // data as those CAN be wild & huge and cause OutOfMemory issues, which we cannot fix in-app
                    // as these buggers load as part of the init phase. :-(
                    //lda_analysis = new LDAAnalysis(lda_sampler);
                }

                return lda_analysis;
            }
        }

        [NonSerialized]
        private Color[] colours;
        public Color[] Colours
        {
            get
            {
                if (null == colours)
                {
#if SAMPLE_LOAD_CODE
                    int num_topics = lda_sampler.NumTopics;

                    colours = new Color[num_topics];
                    for (int i = 0; i < num_topics; ++i)
                    {
                        colours[i] = LDAAnalysisTools.GetTopicColour(i, num_topics);
                    }
#endif
                }

                return colours;
            }
        }
    }
}